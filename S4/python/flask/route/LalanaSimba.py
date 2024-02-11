from Produit import *
from connection.Connection import *

class LalanaSimba:
    def getRn(self):
        return self.__rn
    def setRn(self,rn1):
        if not type(rn1) is int or rn1<=0:
            raise Exception("Only Integer allowed")
        else:
            self.__rn=rn1        
    def getPk1(self):
        return self.__pk1
    def setPk1(self,pk1D):
        if not type(pk1D) is int or pk1D<=0:
            raise Exception("Only Integer allowed")
        else:
            self.__pk1=pk1D
    def getPk2(self):
        return self.pk2
    def setPk1(self,pk2A):
        if not type(pk2A) is int or pk2A<=0:
            raise Exception("Only Integer allowed")
        else:
            self.__pk1=pk1A
    def getNiveau(self):
        return self.__niveau
    def setNiveau(self,n):
        if not type(n) is int or n<0 or n>100:
            raise Exception("Niveau invalide")
        else:
            self.__niveau=n
    def getLongueur(self):
        return self.getPk2()-self.getPk1()
    def getLarge(self):
        return self.__large
    def setLarge(self,large1):
        if not type(large1) is float or large1<=0:
            raise Exception("INvalide largeur from lalanaSimba")
        else:
            self.__large=large1
    #CONSTRUCTOR
    def __init__(self,rn,PkDep,pkArr,niveau,largeur):
        self.setRn(rn)
        self.setPkA(pkArr)
        self.setPkD(PkDep)
        self.setNiveau(niveau)
        self.setLarge(largeur)
    #FUNCTIONS
    def getProfondeur(self):
        profondeur=(self.getNiveau())*10/100
        profondeur/=10
        return profondeur
    def getLongM(self):
        profondeur=self.getProfondeur(self.getNiveau())
        response=profondeur*(self.getLongueur()*1000)*self.getLarge()
        return response
    def getPrixDeRevient(self,con,tab,idP):
        m3=Produit.getPrixM3(tab,con,idP)
        valeur=self.getLongM()
        return m3*valeur
    def getDureeTotal(self,con,idP):
        p=Produit()
        dureeFab=p.getDuree(con,idP)
        return dureeFab*self.getLongM()
    def insertLalana(self,con):
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
                sql = "INSERT INTO LalanaSimba VALUES(default,%s,%s,%s,%s,%s)"+str(idP)
                value=(self.getRn(),self.getPk1(),self.getPk2(),self.getNiveau(),self.getLarge())
                cursor.execute(sql,value)
                con.commit()
                resultat=cursor.rowcount             
            except Exception as e:
                con.rollback()
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