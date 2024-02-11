from connection.Connection import *
class Composant:
    #GETTER AND SETTER
    def getIdP(self):
        return self.__idP
    def getIdC(self):
        return self.__idC
    def getQuantite(self):
        return self.__qte
    def getNomC(self):
        return self.__nomC
    def setidP(self,idP1):
        if not type(idP1) is int or idP1<=0:
            raise Exception("Invalid idProduit")
        else:
            self.__idP=idP1
    def setidC(self,idC1):
        if not type(idC1) is int or idC1<=0:
            raise Exception("Invalid idComposant")
        else:
            self.__idC=idC1
    def setQuantite(self,qte1):
        if not type(qte1) is float or qte1<=0:
            raise Exception("Quantity invalid")
        else:
            self.__qte=qte1
    def setNomC(self,nom):
        if not type(nom) is str:
            raise Exception("Name invalid from Composant")
        else:
            self.__nomC=nom        
    #CONSTRUCTORS  
    def __init__(self,idP,idC,nom,qte):
        self.setidP(idP)
        self.setidC(idC)
        self.setNomC(nom)
        self.setQuantite(qte)
    #FUNCTIONS
    def getAllComposant(self,con,idP):
        resultat=[]
        if not type(idP) is int or idP<=0:
            raise Exception("Invalid idProduct")
        else:
            estValid=True
            cursor=None
            try:
                if con==None:
                    con=Connection.getConnect()
                    estValid=False
                cursor=con.cursor()
                sql = "SELECT * from Global WHERE idP="+str(idP)
                print(sql)
                cursor.execute(sql)
                res=cursor.fetchall()    
                for line in res:
                    c=Composant(line[0],line[1],line[2],float(line[3]))
                    resultat.append(c)           
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
        return resultat
    def getPrixComposant(self,con):
        idC=self.getIdC()
        resultat=None
        if not type(idC) is int or idC<=0:
            raise Exception("Invalid idComposant")
        else:
            estValid=True
            cursor=None
            try:
                if con==None:
                    con=Connection.getConnect()
                    estValid=False
                cursor=con.cursor()
                sql = "SELECT prix from Price WHERE idC="+str(idC)+" ORDER BY daty DESC LIMIT 1 "
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
    def getPrixTotal(self,con):
        estValid=True
        if(con==None):
            con=Connection.getConnect()
            estValid=False
        prix=self.getPrixComposant(con)*self.getQuantite()
        if estValid==False:
            con.close()
        return prix