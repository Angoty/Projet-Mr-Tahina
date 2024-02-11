
class Produit:
    def getidProduit(self):
        return self.idProduit

    def setidProduit(self, id):
        if not type(id) is str or len(id)>7:
            raise ("idProduit invalid")
        else:
            self.idProduit=id
    def __init__(self, id):
        self.setidProduit(id)



            def getAllroads(self, con):
        table=[]
        cursor=None
        res=None
        estConnecte=True

        if(con==None):
            con=Connection.getConnect()
            estConnecte=True
        try:
            cursor=con.cursor()
            sql="INSERT INTO Routes VALUES(DEFAULT, %s, %s, %s, %s, %s, %s, %s)"
            parse=(self.getid(), self.getnom(), self.getstarts(), self.getends(), self.getlengths(), self.getdescr(), self.getreg(), self.getcond())
            cursor.execute(sql, parse)
            res=cursor.rowcount
            print(sql)
        except Exception as e:
            raise(e)
        finally:
            if(estConnecte==False):
                res.close()
                cursor.close()
                con.close()
        return table
        