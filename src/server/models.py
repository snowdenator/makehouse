from peewee import *

database = MySQLDatabase('makehouse', **{'charset': 'utf8', 'use_unicode': True, 'host': 'db01', 'user': 'throwaway', 'password': 'throwaway'})

class UnknownField(object):
    def __init__(self, *_, **__): pass

class BaseModel(Model):
    class Meta:
        database = database

class Suppliers(BaseModel):
    supplier_contact = CharField(null=True)
    supplier_name = CharField(null=True)
    supplier_number = AutoField()
    supplier_website = CharField(null=True)

    class Meta:
        table_name = 'suppliers'

class Parts(BaseModel):
    description = CharField(null=True)
    mfg_partnumber = CharField(null=True, unique=True)
    manufacturer = CharField(null=True)
    preferred_supplier = ForeignKeyField(column_name='preferred_supplier', field='supplier_number', model=Suppliers, null=True)
    stock_level = IntegerField(null=True)
    stock_number = AutoField()
    supplier_1 = ForeignKeyField(backref='suppliers_supplier_1_set', column_name='supplier_1', field='supplier_number', model=Suppliers, null=True)
    supplier_1_min_qty = IntegerField(null=True)
    supplier_1_pn = CharField(null=True)
    supplier_1_price = DecimalField(null=True)
    supplier_2 = ForeignKeyField(backref='suppliers_supplier_2_set', column_name='supplier_2', field='supplier_number', model=Suppliers, null=True)
    supplier_2_min_qty = IntegerField(null=True)
    supplier_2_pn = CharField(null=True)
    supplier_2_price = DecimalField(null=True)

    class Meta:
        table_name = 'parts'

class Sessions(BaseModel):
    login_time = IntegerField(null=True)
    session = CharField(column_name='session_id', primary_key=True)
    username = CharField(null=True)

    class Meta:
        table_name = 'sessions'
