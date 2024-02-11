from config.connexion import connex
if __name__ == '__main__':
    db = connex()
    db.connect()
