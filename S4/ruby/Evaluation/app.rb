require 'sinatra'
require './utile/Lalana.rb'
require './utile/Prest.rb'

get '/' do
    @liste=Lalana.getAllRoutes()
    erb File.read(File.join(File.dirname(__FILE__),'views','index.erb'))
    erb :index
end

post '/prestataire' do
    id = params['idRn'].to_i 
    rn = Lalana.getRoad(id)
    @prestataire=Prest.getRapportPrestRn(rn)
    erb File.read(File.join(File.dirname(__FILE__),'views','liste.erb'))
    erb :liste
end
