require 'pg'    
class Connect
    attr_accessor :conn

    def getConnect()
        begin
            if !defined?(@conn)
                @conn = PG::Connection.new(
                    host: "localhost",
                    port: 5432,
                    dbname: "rubylalana",
                    user: "postgres",
                    password: "angoty"
                )
            end
        rescue => error
            puts(error.message)
        end
        return @conn
    end

end