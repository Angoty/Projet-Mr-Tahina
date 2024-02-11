from connection.Connection import *
from Composant import *
from RoadFair import *
from Produit import *

c=Connection.getConnect()
p=Produit()
print(p.getDuree(c,1))
# tab=Composant(1,1,"aa",2.5)
# listeComp=tab.getAllComposant(c,1)
# m3=p.getPrixM3(liste,c,1)
# print(m3)

# roadFair=RoadFair(1,'qq','12','t1','t2',98,120,7.5,23.2,'Fair')
# liste=roadFair.selectByRn(None,'RNP 2')
# print(len(liste))
# profondeur=roadFair.getProfondeur(4)
roadFair=RoadFair.select(c)
print(len(roadFair))
# for i in range(len(liste)):
#     longue=liste[i].getPrixDeRevient(c,listeComp,4)
#     print()
#     print(liste[i].getT1()+" ----- "+liste[i].getT2()+"==="+str(longue)+" Ar"+" duree= "+str(liste[i].getDureeTotal(c,4))+" jours")
#     print()