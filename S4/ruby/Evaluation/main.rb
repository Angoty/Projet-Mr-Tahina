require "./connection/Connect.rb"
require "./utile/Lalana.rb"
require "./utile/Prest.rb"
c=Connect.new
con=c.getConnect()
# tab=Lalana.getAllRoutes(con)
rn=Lalana.getRoad(1)
# puts(tab.getId())
# puts(con)
tab=Prest.getPrestataireBYRn(rn)