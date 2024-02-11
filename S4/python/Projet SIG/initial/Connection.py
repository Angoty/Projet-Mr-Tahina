import psycopg2


class Connection:
    @staticmethod   
    def getConnect():
        try:
            con=None
            if(con==None):
                con = psycopg2.connect(
                    user = "postgres",
                    password = "mdpprom15",
                    host = "localhost",
                    port = "5432",
                    database = "routes"
                )
            print("Connecte")
        except Exception as e:
            raise e
        return con


    
