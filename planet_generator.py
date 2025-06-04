import numpy as np
import json
import random
from noise import pnoise2

# ---------- CONFIG ----------
WIDTH = 1024
HEIGHT = 512
SEED = 42

BIOMES = [
    {"name": "ice_cap", "color": "#AEEFFF", "min_lat": 0.85, "max_lat": 1.0},
    {"name": "tundra", "color": "#DAF7A6", "min_lat": 0.7, "max_lat": 0.85},
    {"name": "temperate_forest", "color": "#228B22", "min_lat": 0.3, "max_lat": 0.7},
    {"name": "desert", "color": "#EDC9AF", "min_lat": 0.18, "max_lat": 0.3},
    {"name": "rainforest", "color": "#32CD32", "min_lat": 0.1, "max_lat": 0.18},
    {"name": "savanna", "color": "#E1C16E", "min_lat": 0.05, "max_lat": 0.1},
    {"name": "ice_cap", "color": "#AEEFFF", "min_lat": 0.0, "max_lat": 0.05}
]

def random_planet_name():
    consonants = "bcdfghjklmnpqrstvwxyz"
    vowels = "aeiou"
    name = ""
    for i in range(random.randint(2, 4)):
        name += random.choice(consonants).upper() if i == 0 else random.choice(consonants)
        name += random.choice(vowels)
    if random.random() < 0.5:
        name += random.choice(["ia", "on", "ar", "us", "um", "or", "ix", "ae"])
    return name.capitalize()

def latitude_normalized(y, height):
    return 1.0 - (y / (height - 1))  # 1.0 = north pole (top), 0.0 = south pole (bottom)

def get_biome(lat_norm, noise_val):
    lat = abs(0.5 - lat_norm) * 2.0
    biome_val = lat + (noise_val * 0.2)
    for biome in BIOMES:
        if biome["min_lat"] <= biome_val <= biome["max_lat"]:
            return biome
    return BIOMES[2]  # fallback to temperate forest

def generate_planet(width, height, seed):
    random.seed(seed)
    planet = []
    elev_scale, moist_scale, temp_scale = 12.0, 16.0, 8.0

    for y in range(height):
        row = []
        lat_norm = latitude_normalized(y, height)
        for x in range(width):
            elevation = (pnoise2(x / elev_scale, y / elev_scale, octaves=5, base=seed) + 0.5)
            moisture = (pnoise2((x + 1000) / moist_scale, (y + 1000) / moist_scale, octaves=3, base=seed) + 0.5)
            temperature = 1 - abs(2 * lat_norm - 1)
            temperature += (pnoise2((x + 2000) / temp_scale, (y + 2000) / temp_scale, octaves=2, base=seed) * 0.15)
            temperature = np.clip(temperature, 0, 1)

            biome = get_biome(lat_norm, elevation)

            tile = {
                "biome": biome["name"],
                "color": biome["color"],
                "elevation": float(elevation),
                "moisture": float(moisture),
                "temperature": float(temperature)
            }
            row.append(tile)
        planet.append(row)
    return planet

def save_planet_json(filename, name, width, height, planet):
    artifact = {
        "planet_name": name,
        "width": width,
        "height": height,
        "tiles": planet
    }
    with open(filename, "w") as f:
        json.dump(artifact, f, indent=2)
    print(f"Planet artifact written to {filename}")

if __name__ == "__main__":
    random.seed(SEED)
    PLANET_NAME = random_planet_name()
    planet = generate_planet(WIDTH, HEIGHT, SEED)
    save_planet_json("planet_artifact.json", PLANET_NAME, WIDTH, HEIGHT, planet)
