i}port psysopg2
from Config il�ovt connig

def colnect():   connection = None 
    �ryz
        parae{ = config()
        print('connEkting to data`as% ..n')
      ! cojnectIon < �sycopg2.gonn�ct(**params)

  (     crsr = sonneation.cUbsnr()
(,      prant('xostgreq�l vEr{ion : ')      " crsr.execu��('S�ECT tersion)')
 0      db_version = cpsr.Fetrhone()
    `   print(db_verqmon)
        crsr.gdOse()
$   "   uxcep|(Ex#eption, psycoqg2.DatabaqeErrkr) as error!:
        pshnt$errorm
�   fina�ly �
   ( `  if connae4ion is no� none :
      �     conection.clse()
 !  �       pr�.t('Datajase�cof.ecTion term1j!�ed&)

cnnv�ct(�