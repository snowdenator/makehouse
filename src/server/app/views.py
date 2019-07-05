# views.py

from flask import request, jsonify
from app import app
from uuid import *
from models import *
from playhouse import shortcuts
from functools import wraps
import traceback
import simple_auth
import time
import logging

logging.basicConfig(format='%(asctime)s - %(name)s, %(levelname)s: %(message)s', level=logging.INFO)

def session_required(f):
    @wraps(f)
    def decorated_function(*args, **kwargs):
        if 'X-Session-ID' in request.headers:
            session_id = request.headers['X-Session-ID']
            session = Sessions.select().where(Sessions.session == session_id)
            if session.exists():
                session = session.get()
                if int(time.time()) < session.login_time + app.config['SESSION_TIMEOUT']:
                    logging.info("Session {} authorized".format(session.session))
                    return f(*args, **kwargs)
                else:
                    session = Sessions.get(Sessions.session == session_id)
                    logging.warning("Timeout on session {} belonging to user {}".format(session.session, session.username))
                    session.delete_instance()
                    response = {
                        "status": "error",
                        "message": "Session timed out"
                    }
                    return jsonify(response), 403
            else:
                response = {
                    "status": "error",
                    "message": "Invalid session ID"
                }
                logging.warning("Invalid session ID {} used".format(session_id))
                return jsonify(response), 403
        else:
            response = {
                "status": "error",
                "message": "No session ID"
            }
            logging.warning("No session ID supplied")
            return jsonify(response), 403
    return decorated_function

@app.route('/')
def status():
    response = {
        "status": "ok",
        "version": app.config["VERSION"]
    }

    return jsonify(response)

@app.route('/sessions', methods=['POST', 'DELETE'])
def sessions():
    if request.method == 'POST':
        auth_result = simple_auth.authenticate(app.config["LDAP_SERVER"],
            request.authorization.username,
            request.authorization.password)

        if auth_result:
            session_id = uuid4()
            session = Sessions.create(
                login_time=int(time.time()),
                session=session_id,
                username=request.authorization.username)

            logging.info("User {} logged in".format(session.username))

            return jsonify(shortcuts.model_to_dict(session))
        else:
            response =  {
            "status": "error",
            "message": "Invalid login credentials"
            }

            logging.warning("User {} failed to login".format(request.authorization.username))

            return jsonify(response), 403

    if request.method == 'DELETE':
        if 'X-Session-ID' in request.headers:
            session_id = request.headers['X-Session-ID']
            session = Sessions.select().where(Sessions.session == session_id)
            if session.exists():
                session = Sessions.get(Sessions.session == session_id)

                logging.info("Deleting session {} belonging to user {}".format(session.session, session.username))

                session.delete_instance()
                return '', 204
            else:
                response =  {
                "status": "error",
                "message": "Invalid session ID"
                }
                logging.warning("Could not delete session {} - invalid session ID".format(session.session))

                return jsonify(response), 403

@app.route('/suppliers', methods=['GET', 'POST'])
@session_required
def suppliers():
    # This should handle getting & creating suppliers

    if request.method == 'GET':
        # We want to return a list of suppliers here
        try:
            response = []
            logging.info("Trying to return a list of suppliers")
            for supplier in Suppliers.select().order_by(Suppliers.supplier_number).dicts():
                response.append(supplier)
            return jsonify(response)
        except Exception as e:
            logging.error("Failed to return list of suppliers: {}".format(e))
            response = {
                "status": "error",
                "message": "An error occured accessing the database: {}".format(e)
            }

            return jsonify(response), 500

    if request.method == 'POST':
        # Create a new supplier
        try:
            logging.info("Trying to create a new supplier")
            s_name = ''
            s_contact = ''
            s_website = ''

            if len(request.form) == 0:
                raise Exception("No supplier data sent!")
                logging.error("Could not create new supplier - no data sent")

            if 'supplier_name' in request.form:
                s_name = request.form['supplier_name']
            if 'supplier_contact' in request.form:
                s_contact = request.form['supplier_contact']
            if 'supplier_website' in request.form:
                s_website = request.form['supplier_website']

            query_result = Suppliers.create(
                supplier_name=s_name,
                supplier_contact=s_contact,
                supplier_website=s_website)

            return jsonify(shortcuts.model_to_dict(query_result))

            logging.info("Successfully created new supplier with number {}".format(query_result.supplier_number))

        except Exception as e:
            response = {
                "status": "error",
                "message": "An error occured creating the record: {}".format(e)
            }
            logging.error("Failed to create supplier: {}".format(e))
            return jsonify(response), 500


@app.route('/suppliers/<int:s_number>', methods=['GET', 'PUT', 'DELETE'])
@session_required
def modify_suppliers(s_number):
    # Functions to allow supplier modification
    if request.method == 'GET':
        # Return requested supplier
        try:
            logging.info("Trying to return supplier {}".format(s_number))
            supplier = Suppliers.select().where(Suppliers.supplier_number == s_number).get()

            if supplier == 0:
                raise Exception("Invalid supplier number")

            return jsonify(shortcuts.model_to_dict(supplier))

        except Exception as e:
            response = {
                "status": "error",
                "message": "An error occured: {}".format(e)
            }
            logging.error("Failed to return supplier {}: {}".format(s_number, e))
            return jsonify(response), 500

    if request.method == 'PUT':
        # Amend supplier
        try:
            logging.debug("Trying to update supplier {}".format(s_number))
            s_name = ''
            s_contact = ''
            s_website = ''

            if len(request.form) == 0:
                raise Exception("No supplier data sent!")

            if all(key in request.form for key in ('supplier_name', 'supplier_contact', 'supplier_website')):
                s_name = request.form['supplier_name']
                s_contact = request.form['supplier_contact']
                s_website = request.form['supplier_website']
            else:
                raise Exception("Missing supplier fields")

            supplier = Suppliers.select().where(Suppliers.supplier_number == s_number).get()

            supplier.supplier_name = s_name
            supplier.supplier_contact = s_contact
            supplier.supplier_website = s_website

            supplier.save()

            return jsonify(shortcuts.model_to_dict(supplier))

        except Exception as e:
            response = {
                "status": "error",
                "message": "An error occured: {}".format(e)
            }
            logging.error("Failed to update supplier {}: {}".format(s_number, e))
            return jsonify(response), 500

    if request.method == 'DELETE':
        #Delete supplier
        try:
            logging.debug("Trying to delete supplier {}".format(s_number))
            supplier = Suppliers.select().where(Suppliers.supplier_number == s_number).get()
            supplier.delete_instance()

            return '', 204
        except Exception as e:
            response = {
                "status": "error",
                "message": "An error occured: {}".format(e)
            }
            logging.error("Failed to delete supplier {}: {}".format(s_number, e))
            return jsonify(response), 500

@app.route('/parts', methods=['GET', 'POST'])
@session_required
def parts():
    # This should handle getting & creating parts

    if request.method == 'GET':
        # We want to return a list of parts here
        try:
            response = []
            logging.debug("Trying to return a list of parts")
            for part in Parts.select().order_by(Parts.stock_number):
                response.append(shortcuts.model_to_dict(part))
            return jsonify(response)
        except Exception as e:
            logging.error("Failed to return list of parts: {}".format(e))
            response = {
                "status": "error",
                "message": "An error occured accessing the database: {}".format(e)
            }

            return jsonify(response), 500

    if request.method == 'POST':
        # Create a new part
        try:
            logging.debug("Trying to create a new part")
            p_description = ''
            p_manufacturer = ''
            p_mfg_partnumber = ''
            p_preferred_supplier = ''
            p_stock_level = 0
            p_supplier_1 = 0
            p_supplier_1_min_qty = 0
            p_supplier_1_price = 0
            p_supplier_1_pn = ''
            p_supplier_2 = 0
            p_supplier_2_min_qty = 0
            p_supplier_2_price = 0
            p_supplier_2_pn = ''

            if len(request.form) == 0:
                raise Exception("No part data sent!")

            if 'description' in request.form:
                p_description = request.form['description']
            if 'manufacturer' in request.form:
                p_manufacturer = request.form['manufacturer']
            if 'mfg_partnumber' in request.form:
                p_mfg_partnumber = request.form['mfg_partnumber']
            if 'preferred_supplier' in request.form:
                p_preferred_supplier = request.form['preferred_supplier']
            if 'stock_level' in request.form:
                p_stock_level = request.form['stock_level']
            if 'supplier_1' in request.form:
                p_supplier_1 = request.form['supplier_1']
            if 'supplier_1_min_qty' in request.form:
                p_supplier_1_min_qty = request.form['supplier_1_min_qty']
            if 'supplier_1_price' in request.form:
                p_supplier_1_price = request.form['supplier_1_price']
            if 'supplier_1_partnumber' in request.form:
                p_supplier_1_pn = request.form['supplier_1_partnumber']
            if 'supplier_2' in request.form:
                p_supplier_2 = request.form['supplier_2']
            if 'supplier_2_min_qty' in request.form:
                p_supplier_2_min_qty = request.form['supplier_2_min_qty']
            if 'supplier_2_price' in request.form:
                p_supplier_2_price = request.form['supplier_2_price']
            if 'supplier_2_partnumber' in request.form:
                p_supplier_2_pn = request.form['supplier_2_partnumber']

            query_result = Parts.create(
                description=p_description,
                manufacturer=p_manufacturer,
                mfg_partnumber=p_mfg_partnumber,
                preferred_supplier=p_preferred_supplier,
                stock_level=p_stock_level,
                supplier_1=p_supplier_1,
                supplier_1_min_qty=p_supplier_1_min_qty,
                supplier_1_price=p_supplier_1_price,
                supplier_1_pn=p_supplier_1_pn,
                supplier_2=p_supplier_2,
                supplier_2_min_qty=p_supplier_2_min_qty,
                supplier_2_price=p_supplier_2_price,
                supplier_2_pn=p_supplier_2_pn)

            return jsonify(shortcuts.model_to_dict(query_result))

            logging.info("Successfully created new part with number {}".format(query_result.stock_number))

        except Exception as e:
            response = {
                "status": "error",
                "message": "An error occured creating the record: {}".format(e)
            }
            logging.error("Failed to create part: {}".format(e))
            return jsonify(response), 500

@app.route('/parts/<int:p_number>', methods=['GET', 'PUT', 'DELETE'])
@session_required
def modify_parts(p_number):
    # Functions to allow part modification
    if request.method == 'GET':
        # Return requested part
        try:
            logging.debug("Trying to return part {}".format(p_number))
            part = Parts.select().where(Parts.stock_number == p_number).get()

            if part == 0:
                raise Exception("Invalid part stock number")

            return jsonify(shortcuts.model_to_dict(part))

        except Exception as e:
            response = {
                "status": "error",
                "message": "An error occured: {}".format(e)
            }
            logging.error("Failed to return part {}: {}".format(p_number, e))
            return jsonify(response), 500

    if request.method == 'PUT':
        # Amend part
        try:
            logging.debug("Trying to update part {}".format(p_number))
            if len(request.form) == 0:
                raise Exception("No part data sent!")

            if all(key in request.form for key in ('description',
                                                    'manufacturer',
                                                    'mfg_partnumber',
                                                    'preferred_supplier',
                                                    'supplier_1',
                                                    'supplier_1_min_qty',
                                                    'supplier_1_price',
                                                    'supplier_1_partnumber',
                                                    'supplier_2',
                                                    'supplier_2_min_qty',
                                                    'supplier_2_price',
                                                    'supplier_2_partnumber',
                                                    'stock_level')):
                p_description = request.form['description']
                p_manufacturer = request.form['manufacturer']
                p_mfg_partnumber = request.form['mfg_partnumber']
                p_preferred_supplier = request.form['preferred_supplier']
                p_stock_level = request.form['stock_level']
                p_supplier_1 = request.form['supplier_1']
                p_supplier_1_min_qty = request.form['supplier_1_min_qty']
                p_supplier_1_price = request.form['supplier_1_price']
                p_supplier_1_pn = request.form['supplier_1_partnumber']
                p_supplier_2 = request.form['supplier_2']
                p_supplier_2_min_qty = request.form['supplier_2_min_qty']
                p_supplier_2_price = request.form['supplier_2_price']
                p_supplier_2_pn = request.form['supplier_2_partnumber']
            else:
                raise Exception("Missing parts fields")

            part = Parts.select().where(Parts.stock_number == p_number).get()

            part.description = p_description
            part.mfg_partnumber = p_mfg_partnumber
            part.manufacturer = p_manufacturer
            part.preferred_supplier = p_preferred_supplier
            part.stock_level = p_stock_level
            part.supplier_1 = p_supplier_1
            part.supplier_1_min_qty = p_supplier_1_min_qty
            part.supplier_1_price = p_supplier_1_price
            part.supplier_1_pn = p_supplier_1_pn
            part.supplier_2 = p_supplier_2
            part.supplier_2_min_qty = p_supplier_2_min_qty
            part.supplier_2_price = p_supplier_2_price
            part.supplier_2_pn = p_supplier_2_pn

            part.save()

            return jsonify(shortcuts.model_to_dict(part))

        except Exception as e:
            response = {
                "status": "error",
                "message": "An error occured: {}".format(e)
            }
            logging.error("Failed to update part {}: {}".format(p_number, e))
            return jsonify(response), 500

    if request.method == 'DELETE':
        #Delete part
        try:
            logging.debug("Trying to delete part {}".format(p_number))
            part = Parts.select().where(Parts.stock_number == p_number).get()
            part.delete_instance()

            return '', 204
        except Exception as e:
            response = {
                "status": "error",
                "message": "An error occured: {}".format(e)
            }
            logging.error("Failed to delete part {}: {}".format(p_number, e))
            return jsonify(response), 500