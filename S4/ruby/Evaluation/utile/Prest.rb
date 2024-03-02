class Prest
    attr_accessor :idPrest,:nomPrest,:prixM3,:vit,:emp,:anc,:quality

    def initialize(idPrest1,nomPrest1,prixM31,vit1,emp1,anc1)
        @idPrest=idPrest1
        @nomPrest=nomPrest1
        @prixM3=prixM31
        @vit=vit1
        @emp=emp1
        @anc=anc1
    end

    def getIdPrest()
        return @idPrest
    end
    
    def getNomPrest()
        return @nomPrest
    end

    def getPrixM3()
        return @prixM3
    end
    
    def getVit()
        return @vit
    end
    
    def getEmp()
        return @emp
    end
    def getAnc()
        return @anc
    end
    def getQualityByRn()
        return @quality
    end

    def setIdPrest(id)
        if !defined?(id)
            raise Exception.new "IdPrest invalide"
        end
        @idPrest=id
    end

    def setNomPrest(name)
        if !defined?(name)
            raise Exception.new "NomPrest invalide"
        end
        @nomPrest=name
    end

    def setPrixM3(price)
        if !defined?(price)
            raise Exception.new "Prixm3 invalide"
        end
        @prixM3=price
    end

    def setVit(vitesse)
        if !defined?(vitesse)
            raise Exception.new "Vitesse invalide"
        end
        @vit=vitesse
    end

    def setEmp(emp1)
        if !defined?(emp1)
            raise Exception.new "Emp invalide"
        end
        @emp=emp1
    end

    def setAnc(anc1)
        if !defined?(anc1)
            raise Exception.new "Anc invalide"
        end
        @anc=anc1
    end

    def setQualityByRn(qualiter)
        if !defined?(qualiter)
            raise Exception.new "Qualite invalide"
        end
        @quality=qualiter
    end

    def self.getPrestataireBYRn(rn,con=0)
        tab=[]
        estValid=true
        begin         
            if con==0
                c=Connect.new
                con=c.getConnect()
                estValid=false
            end
            qualite=rn.getQualite()
            res=con.exec("SELECT*,#{qualite}  qualite FROM prest Order BY #{qualite} DESC")
            puts("SELECT*,#{qualite} qualite FROM prest Order BY #{qualite} DESC")
            res.each do |row|
                puts(row)
                prest=Prest.new(row['idprest'],row['nomprest'],row['prixm3'],row['vit'],row['emp'],row['anc'])
                prest.setQualityByRn(row['qualite'])
                # puts(prest.getQualityByRn())
                tab<<prest
            end
            if estValid==false
                con.close()
            end
        rescue => error
            puts(error.message)
        end
        return tab
    end

    def self.getRapportPrestRn(rn,con=0)
        tab=[]
        estValid=true
        begin         
            if con==0
                c=Connect.new
                con=c.getConnect()
                estValid=false
            end
            qualite=rn.getQualite()
            res=con.exec("SELECT*,(#qualite})/prixm3 prix FROM prest Order BY (#{qualite})/prixm3 DESC")
            res.each do |row|
                puts(row)
                prest=Prest.new(row['idprest'],row['nomprest'],row['prixm3'],row['vit'],row['emp'],row['anc'])
                prest.setQualityByRn(row['prix'])
                # puts(prest.getQualityByRn())
                tab<<prest
            end
            if estValid==false
                con.close()
            end
        rescue => error
            puts(error.message)
        end
        return tab
    end
end