from connexion.Connection import connection
class Niveau :
    def getId() :
        return self.id
    def getNom() :
        return self.nom
    def getHauteur():
        return self.hauteur
    def setId(id):
        self.id = id
    def setNom(nom):
        self.nom = nom
    def setValeur(valeur):
        if valeur <=0 :
            raise Exception("valeur de niveau invalid")
        else :
            self.valeur = valeur
    def __init__(self,id,nom,valeur):
        self.setId(id)
        self.setNom(nom)
        self.setValeur(valeur)
    
    def select_niveau(con,idNiveau) :
        valid = 1 
        allNiveau = []
        ind = 0 
        try : 
            if con==None :
                con = Connection.getConnection()
                valid = -1
            cursor = con.cursor() 
            cursor.execute("SELECT * FROM niveau where id = "+idNiveau+"")
            result = cursor.fetchall()
            for res in result :
                allNiveau[ind] = Niveau(res[0],res[1],res[2])
                ind = ind +1
        except Exception as exe :
            raise Exception(exe+"est l'erreur")
        finally:
            if valid == -1 and con!=None :
                con.close()

