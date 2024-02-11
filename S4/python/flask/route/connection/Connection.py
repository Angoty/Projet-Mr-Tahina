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
                    database = "lalana"
                )
            print("Connecter")
        except Exception as e:
            raise e
        # print(con)
        return con