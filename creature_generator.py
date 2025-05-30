import random
import json

NUM_PREDATOR_SPECIES = 10
NUM_HERBIVORE_SPECIES = 10
INDIVIDUALS_PER_SPECIES = 10
BODY_SIZES = [1, 2, 4]  # 1x1, 2x2, 4x4

# Themed color palettes (hex codes)
PREDATOR_COLORS = [
    ["#FF3A3A", "#FF8C00", "#FFD700", "#FF1493", "#FF4500"],  # Red, orange, yellow, pink, orange-red
    ["#FF1E56", "#FFAC41", "#FFE156", "#FF6263", "#FFCD38"],  # Neon pink, orange, yellow, etc.
    ["#FF2079", "#FFC300", "#FF5733", "#C70039", "#900C3F"],
    ["#FF3031", "#FD5E53", "#FFB7B2", "#F4845F", "#C9184A"],
    ["#D7263D", "#F46036", "#2E294E", "#1B998B", "#C5D86D"],
    ["#F72585", "#720026", "#CE4257", "#FFB997", "#FFA5A5"],
    ["#FF3366", "#FF6F61", "#FFD700", "#F7B801", "#F18701"],
    ["#EA3546", "#F86624", "#F9C80E", "#43BCCD", "#662E9B"],
    ["#FA8334", "#F9C80E", "#F4D35E", "#70C1B3", "#B2DBBF"],
    ["#F94144", "#F3722C", "#F9C74F", "#90BE6D", "#577590"],
]

HERBIVORE_COLORS = [
    ["#7C9473", "#A5C882", "#E6E6E6", "#B8B8B8", "#8E9B97"],   # Olive, light green, greys
    ["#C4B7CB", "#A3B18A", "#D9BF77", "#B5CDA3", "#B0A990"],   # Lavender, khaki, olive, tan
    ["#A6A57A", "#CEB992", "#ECE4B7", "#C9BBCF", "#2D3047"],   # Soft yellow, beige, purple
    ["#A9BDBF", "#6B4226", "#D9BF77", "#A2A392", "#7B9EA8"],   # Greys, browns, blue-greys
    ["#556B2F", "#8F9779", "#B8B08D", "#797B3A", "#A7BEAE"],   # Earthy greens, moss
    ["#8A817C", "#A1C181", "#BCCCA8", "#B7B7A4", "#D6DBB2"],
    ["#78866B", "#B4A582", "#C0B283", "#D7BEA8", "#A68A64"],
    ["#62760C", "#C6D166", "#C7EFCF", "#EEF5DB", "#FFE156"],
    ["#A5A58D", "#6D6875", "#B5838D", "#FFB4A2", "#E5989B"],
    ["#A3B18A", "#B5CDA3", "#9C6644", "#6F1E51", "#B9E4C9"],
]

PATTERNS = ["solid", "stripes", "spots", "bands", "mottled"]

def random_species_name(prefix):
    # Just mix a prefix with a random ending for alien-sounding names
    syllables = ["ron", "lus", "dra", "vik", "zar", "len", "bis", "tis", "lak", "zel"]
    return prefix + random.choice(syllables) + random.choice(syllables)

def random_biome():
    return random.choice(["ice_cap", "tundra", "temperate_forest", "desert", "rainforest", "savanna"])

def make_species(species_id, is_predator, palette):
    return {
        "species_id": species_id,
        "is_predator": is_predator,
        "palette": palette
    }

def generate_individual(species, gender, index):
    size = random.choice(BODY_SIZES)
    color = random.choice(species["palette"])
    limb_count = random.randint(2, 8)
    pattern = random.choice(PATTERNS)
    stats = {
        "health": random.randint(20, 100) * size,
        "speed": random.uniform(0.5, 2.5) / size,
        "strength": random.randint(5, 30) * size if species["is_predator"] else random.randint(2, 15) * size,
        "max_age": random.randint(20, 60),
        "hunger": random.randint(5, 25),
    }
    name = f"{species['species_id']}-{index:03d}{gender}"
    return {
        "name": name,
        "species_id": species["species_id"],
        "is_predator": species["is_predator"],
        "gender": gender,
        "body_size": size,
        "color": color,
        "pattern": pattern,
        "limb_count": limb_count,
        "stats": stats,
        "biome": random_biome(),
    }

def main():
    random.seed(42)  # Deterministic for repeatability
    all_creatures = []

    # Generate predator species
    predator_species = []
    for i in range(NUM_PREDATOR_SPECIES):
        species_id = random_species_name("Pred")
        palette = PREDATOR_COLORS[i % len(PREDATOR_COLORS)]
        predator_species.append(make_species(species_id, True, palette))

    # Generate herbivore species
    herbivore_species = []
    for i in range(NUM_HERBIVORE_SPECIES):
        species_id = random_species_name("Herb")
        palette = HERBIVORE_COLORS[i % len(HERBIVORE_COLORS)]
        herbivore_species.append(make_species(species_id, False, palette))

    # Make individuals
    for sidx, species in enumerate(predator_species):
        for i in range(INDIVIDUALS_PER_SPECIES):
            gender = random.choice(["M", "F"])
            creature = generate_individual(species, gender, i)
            all_creatures.append(creature)
    for sidx, species in enumerate(herbivore_species):
        for i in range(INDIVIDUALS_PER_SPECIES):
            gender = random.choice(["M", "F"])
            creature = generate_individual(species, gender, i)
            all_creatures.append(creature)

    # Save to file
    with open("creatures.json", "w") as f:
        json.dump(all_creatures, f, indent=2)
    print("Generated and saved 200 creatures to creatures.json")

if __name__ == "__main__":
    main()
