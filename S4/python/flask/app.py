from flask import Flask, render_template, request, url_for, redirect
from route.connection.Connection import *
from route.RoadFair import *
import geopandas as gpd


app = Flask(__name__)


@app.route('/')
def index():
    db = Connection.getConnect()
    rn = RoadFair.select(db)

    return render_template('index.html', rn=rn)


# @app.route('/map')
# def carte():
#     init = connex()
#     db = init.connect_to_db()
#     sql = text("SELECT * FROM madagascar_roads_version4")
#     maps = gpd.GeoDataFrame.from_postgis(sql, db, geom_col='geom')
#     map_options = {
#         'center': [-18.9333, 47.5167],
#         'zoom': 6
#     }
#     maps.set_crs(epsg=4326, inplace=True)
#     map_geojson = maps.to_crs(epsg=4326).to_json()

#     return render_template('map.html', map_geojson=map_geojson, map_options=map_options)


@app.route('/process_form', methods=['POST'])
def process():
    # profondeur = fonctions.profondeur(10, 10, 4)
    # longeur = fonctions.longeur(pk_a, pk_d)
    # volume = fonctions.get_volume(longeur, profondeur)
    # cout_reparation = fonctions.cout_reparation(volume, surftype)
    # estimation = fonctions.estimation_reparation(volume, 10, 20)
    # prix_reviens = fonctions.prix_reviens(cout_reparation, volume)
    # fonctions.insert_simba(roadno, pk_d, pk_a, 4, largeur, surftype)
    # fonctions.insert_cout_reparation(roadno, estimation)

    return redirect(url_for('confirmed'))

# @app.route('/confirmed')
# def confirmed():
#     return render_template('confirmed.html')


if __name__ == '__main__':
    app.run(debug=True)
