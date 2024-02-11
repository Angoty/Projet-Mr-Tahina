class LalanaSimba :
    def getId():
        return self.id
    
    def getPk_depart():
        return self.pk_depart 
    
    def getPk_arriver():
        return self.pk_arriver

    def getIdNiveau():
        return self.idNiveau
    
    def setId(id):
        self.id = id 
    
    def setPk_arriver(pk_arriver):
        if pk_arriver <=0 :
            raise Exception("Point kilometrique d'arriver invalid ")
        else :
            self.pk_arriver = pk_arriver
    def setPk_depart(pk_depart):
        if pk_depart <=0 :
            raise Exception("Point kilometrique de depart invalid")
        else :
            self.pk_depart = pk_depart
    def setIdNiveau(niveau):
        if niveau <=0 :
            raise Exception("Niveau invalid")
        else :
            self.idNiveau = niveau
    
    def __init__(self,id,pk_depart,pk_arriver,idNiveau):
        self.setId(id)
        self.setPk_depart(pk_depart)
        self.setPk_arriver(pk_arriver)
        self.setIdNiveau(idNiveau)
    
    def calcul_simba(longueur , hauteur ,largeur) :
        return (hauteur/100) * largeur * (longueur*1000)
    


    