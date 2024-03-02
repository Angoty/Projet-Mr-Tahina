require './connection/Connect.rb'
class Lalana
    attr_accessor :id,:routes,:longueur,:largeur,:niveau

    def getId()
        return @id
    end

    def getRoutes()
        return @routes
    end

    def getLongueur()
        return @longueur
    end

    def getLargeur()
        return @largeur
    end

    def getNiveau()
        return @niveau
    end

    def setId(id1)
        if !defined?(id1)
            raise Exception.new "id invalide"
        end
        @id=id1
    end

    def setRoutes(routes1)
        if !defined?(routes1)
            raise Exception.new "routes invalide"
        end
        @routes=routes1
    end

    def setLongueur(longueur1)
        if !defined?(longueur1)
            raise Exception.new "longueur invalide"
        end
        @longueur=longueur1
    end

    def setLargeur(largeur1)
        if !defined?(largeur1)
            raise Exception.new "largeur invalide"
        end
        @largeur=largeur1
    end

    def setNiveau(niveau1)
        if !defined?(niveau1)
            raise Exception.new "Niveau invalide"
        end
        @niveau=niveau1
    end

    def initialize(id1,routes1,longueur1,largeur1,niveau1)
        @id=id1
        @routes=routes1
        @longueur=longueur1
        @largeur=largeur1
        @niveau=niveau1
    end

    def getQualite(con=0)
        estValid=true
        qualite=0
        begin
            if con==0
                c=Connect.new
                con=c.getConnect()
                estValid=false
            end
            res=con.exec("SELECT qualite FROM donnes WHERE idLalana="+getId().to_s)
            puts("SELECT qualite FROM donnes WHERE idLalana="+getId().to_s)
            res.each do |row|
                qualite=row['qualite']
            end
            if estValid==false
                con.close()
            end
        rescue => error
            puts(error.message)
        end
        return qualite        
    end

    def self.getAllRoutes(con=0)
        estValid=true
        tab=[]
        begin
            if con==0
                c=Connect.new
                con=c.getConnect()
                estValid=false
            end
            res=con.exec("SELECT*FROM lalana")
            res.each do |row|
                route=Lalana.new(row['id'],row['routes'],row['longueur'],row['largeur'],row['niveau'])
                tab<<route
            end
            if estValid==false
                con.close()
            end
        rescue => error
            puts(error.message)
        end
        return tab
    end

    def self.getRoad(id,con=0)
        estValid=true
        begin
            if con==0
                c=Connect.new
                con=c.getConnect()
                estValid=false
            end
            res=con.exec("SELECT*FROM lalana WHERE id="+id.to_s)
            res.each do |row|
                route=Lalana.new(row['id'],row['routes'],row['longueur'],row['largeur'],row['niveau'])
                return route
            end
            if estValid==false
                con.close()
            end
        rescue => error
            puts(error.message)
        end
        return false
    end

    def getProfondeur()
        niveau=(@niveau)/10
        prof=niveau*(@longueur*1000)*@largeur
        return prof
    end
end
