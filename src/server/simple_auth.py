# LDAP authentication module

import ldap

def authenticate(address, username, password):
    conn = ldap.initialize('ldap://' + address)
    conn.protocol_version = 3
    conn.set_option(ldap.OPT_REFERRALS, 0)

    try:
        result = conn.simple_bind_s(username, password)
    except Exception as e:
        return False
    finally:
        conn.unbind_s()

    return True