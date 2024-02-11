class Matiere :
    def getId() :
        return self.id
    def getNom() :
        return self.nom
    def setId(id):
        self.id = id
    def setNom(nom):
        self.nom = nom
    def __init__(self,id,nom):
        self.setId(id)
        self.setNom(nom)