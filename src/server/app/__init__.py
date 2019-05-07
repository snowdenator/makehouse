# app/__init__.py

from flask import Flask

# App initialisation
app = Flask(__name__, instance_relative_config=True)

# Import views
from app import views

# Import config
app.config.from_object('config')
