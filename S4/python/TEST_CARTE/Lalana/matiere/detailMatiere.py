class Detail :
    def getIdMatiere() :
        return self.idMatiere
    def getvaleur() :
        return self.valeur
    def getDuree():
        return self.duree
    def setId(id):
        self.idMatiere = idMatiere
    def setvaleur(valeur):
        if valeur >=0 :
            self.valeur = valeur
        else :
            raise Exception("prix invalid")
    def setDuree(duree):
        if duree <=0 :
            raise Exception("Duree invalid")
        else :
            self.duree = duree

    def __init__(self,idMatiere,valeur):
        self.setidMatiere(idMatiere)
        self.setvaleur(valeur)
        
    def getSumPrice(valeurM):
        return valeurM * self.getvaleur()
    
    def getSumDuree(valeurM):
        return valeurM * self.getDuree() 
    