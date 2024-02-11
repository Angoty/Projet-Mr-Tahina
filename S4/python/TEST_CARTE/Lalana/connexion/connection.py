import psycopg2 
class Connection :
    @staticmethod
    def getConnection():
        try: 
            con = psycopg2.connect(
            database = "test" ,
            user = "postgres" ,
            password = "mdpprom15" ,
            host = "localhost" ,
            port = "5432"
            )
            print("connect")
        except Exception as exe:
            raise exe
        return con