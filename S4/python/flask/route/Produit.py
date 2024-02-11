class Produit:
    def getIdP(self):
        return self.__idP
    def setidP(self,idP1):
        if idP1<=0 or not type(pkArriver) is int:
            raise Exception("IdProduct invalid")
        else:
            self.__idP=idP1
    def getNomP(self):
        return self.__nomP
    def setnomP(self,nomP1):
        if not type(nomP1) is str:
            raise Exception("NomProduct invalid")
        else:
            self.__nomP=nomP1
    # def __init__(self,idP1,nomP1):
    #     self.setNomP(nomP1)
    #     self.setidP(idP1)
    @staticmethod
    def getPrixM3(tab,con,idP):
        prix=0
        for i in range(len(tab)):
            prix+=tab[i].getPrixTotal(con)
        return prix
    def getDuree(self,con,idP):
        resultat=None
        if not type(idP) is int or idP<=0:
            raise Exception("Invalid idProduit")
        else:
            estValid=True
            cursor=None
            try:
                if con==None:
                    con=Connection.getConnect()
                    estValid=False
                cursor=con.cursor()
                sql = "SELECT dureeFab from Produit WHERE idP="+str(idP)
                print(sql)
                cursor.execute(sql)
                resultat=cursor.fetchone()             
            except Exception as e:
                raise e
            finally:
                if cursor!=None:
                    try:
                        cursor.close()
                    except Exception as ex:
                        raise ex
                if estValid==False:
                    try:
                        con.close()
                    except Exception as exe:
                        raise exe
        return resultat[0]