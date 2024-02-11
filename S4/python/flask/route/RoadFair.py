from route.connection.Connection import *
from route.Produit import *
class RoadFair:
    def getIdr(self):
        return self.__idr
    def getRn(self):
        return self.__rn
    def getLink(self):
        return self.__link
    def getT1(self):
        return self.__t1
    def getT2(self):
        return self.__t2
    def getPkD(self):
        return self.__pkD
    def getPkA(self):
        return self.__pkA
    def getLong(self):
        return self.__long
    def getLarge(self):
        return self.__large
    def getNiveau(self):
        return self.__niveau
    def getCondition(self):
        return self.__cond
    def setIdr(self,id1):
        if not type(id1) is int or id1<=0:
            raise Exception("INvalide gid from roadFair")
        else:
            self.__idr=id1
    def setRn(self,rn1):
        if not type(rn1) is str:
            raise Exception("INvalide rn from roadFair")
        else:
            self.__rn=rn1
    def setLink(self,link1):
        if not type(link1) is str:
            raise Exception("INvalide linkno from roadFair")
        else:
            self.__link=link1
    def setT1(self,t11):
        if not type(t11) is str:
            raise Exception("INvalide toerana1 from roadFair")
        else:
            self.__t1=t11
    def setT2(self,t22):
        if not type(t22) is str:
            raise Exception("INvalide toerana2 from roadFair")
        else:
            self.__t2=t22
    def setPkD(self,pk1):
        if not type(pk1) is int or pk1<0:
            raise Exception("INvalide start_km 222 from roadFair")
        else:
            self.__pkD=pk1
    def setPkA(self,pk2):
        if not type(pk2) is int or pk2<0:
            raise Exception("INvalide end_km from roadFair")
        else:
            self.__pkA=pk2
    def setLong(self,l):
        if not type(l) is float or l<=0:
            raise Exception("INvalide longueur from roadFair")
        else:
            self.__long=l
    def setLarge(self,large1):
        if not type(large1) is float or large1<0:
            raise Exception("INvalide largeur from roadFair")
        else:
            self.__large=large1
    def setCond(self,cond1):
        if not type(cond1) is str:
            raise Exception("Invalide condition from roadFair")
        else:
            self.__cond=cond1
    def setNiveau(self,n):
        if not type(n) is int or n<0:
            raise Exception("INvalide end_km from roadFair")
        else:
            self.__niveau=n
    def __init__(self,gid,rn,link,t1,t2,pk1,pk2,longueur,largeur,condition):
        self.setIdr(gid)
        self.setRn(rn)
        self.setLink(link)
        self.setT1(t1)
        self.setT2(t2)
        self.setPkD(pk1)
        self.setPkA(pk2)
        self.setLong(longueur)
        self.setLarge(largeur)
        self.setCond(condition) 
    def selectByRn(self,con,rnS):
        resultat=[]
        if not type(rnS) is str:
            raise Exception("Invalid Roadno")
        else:
            estValid=True
            cursor=None
            try:
                if con==None:
                    con=Connection.getConnect()
                    estValid=False
                cursor=con.cursor()
                sql = "SELECT * from RoadFair WHERE rn='"+rnS+"'"
                cursor.execute(sql)
                res=cursor.fetchall()
                print(sql)    
                for line in res:
                    # print(line)
                    road=RoadFair(int(line[0]),str(line[1]),str(line[2]),str(line[3]),str(line[4]),int(line[5]),int(line[6]),float(line[7]),float(line[8]),str(line[9]))
                    resultat.append(road)         
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
    @staticmethod
    def select(con):
        resultat=[]
        estValid=True
        cursor=None
        try:
            if con==None:
                con=Connection.getConnect()
                estValid=False
            cursor=con.cursor()
            sql = "SELECT * from RoadFair"
            cursor.execute(sql)
            res=cursor.fetchall()
            print(sql)    
            for line in res:
                # print(line[8])
                road=RoadFair(int(line[0]),str(line[1]),str(line[2]),str(line[3]),str(line[4]),int(line[5]),int(line[6]),float(line[7]),float(line[8]),str(line[9]))
                resultat.append(road)         
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
    def getProfondeur(self,niveau):
        profondeur=(niveau)*10/100
        profondeur/=10
        return profondeur
    def getLongM(self,niveau):
        profondeur=self.getProfondeur(niveau)
        response=profondeur*(self.getLong()*1000)*self.getLarge()
        return response
    def getCout(self,con,tab,niveau):
        m3=Produit.getPrixM3(tab,con,1)
        valeur=self.getLongM(niveau)
        return m3*valeur
    def getPrixDeRevient(self,con,tab,niveau):
        m3=Produit.getPrixM3(tab,con,1)
        valeur=self.getLongM(niveau)
        return self.getCout(con,tab,niveau)/valeur
    def getDureeTotal(self,con,niveau):
        p=Produit()
        dureeFab=p.getDuree(con,1)
        return dureeFab*self.getLongM(niveau)