import folium

# Création de la carte centrée sur Paris
map = folium.Map(location=[48.8566, 2.3522], zoom_start=13)

# Ajout d'un marqueur sur la Tour Eiffel
folium.Marker(location=[48.8584, 2.2945], popup="Tour Eiffel").add_to(map)

# Affichage de la carte
map.save('carte.html')  # sauvegarder la carte en HTML
