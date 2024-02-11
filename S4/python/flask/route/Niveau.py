class Niveau:
    def getIdN():
        return self.__id
    def getNomN():
        return self.__nom
    def getValueNiveau():
        return self.__valeur
    def setIdN(self,idN):
        if not type(idN) is int or idN<=0:
            raise Exception("IdNiveau not valid")
        else:
            self.__id=idN
    def setNomN(self,nomN):
        if not type(nomN) is str:
            raise Exception("NOm niveau non valide")
        else:
            self.__nom=nomN
    def setValueNiveau(self,valeur1):
        if not type(valeur1) is int or not type(valeur1) is float or valeur1<=0:
            raise Exception("Valeur non valide")
        else:
            self.__valeur=valeur1