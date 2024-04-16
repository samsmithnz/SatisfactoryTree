# SatisfactoryTree


[![CI/CD](https://github.com/samsmithnz/SatisfactoryTree/actions/workflows/workflow.yml/badge.svg)](https://github.com/samsmithnz/SatisfactoryTree/actions/workflows/workflow.yml)
[![Coverage Status](https://coveralls.io/repos/github/samsmithnz/SatisfactoryTree/badge.svg?branch=main)](https://coveralls.io/github/samsmithnz/SatisfactoryTree?branch=main)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=samsmithnz_SatisfactoryTree&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=samsmithnz_SatisfactoryTree)
[![Current Release](https://img.shields.io/github/release/samsmithnz/SatisfactoryTree/all.svg)](https://github.com/samsmithnz/SatisfactoryTree/releases)


A POC project to graph [Satisfactory](https://store.steampowered.com/app/526870/Satisfactory/) items with [D3](https://d3js.org/) [force directed graphs](https://en.wikipedia.org/wiki/Force-directed_graph_drawing). 
Sister project to [http://github.com/samsmithnz/SatisfactoryPlan](http://github.com/samsmithnz/SatisfactoryPlan)

Also has a production calculator that outputs mermaid graphs.
```mermaid
flowchart LR
    ReinforcedIronPlate["x2.4 Assembler<br>(Reinforced Iron Plate)"]
    IronPlate["x3.6 Constructor<br>(Iron Plate)"]
    IronIngot["x4.8 Smelter<br>(Iron Ingot)"]
    IronOre["x2.4 MiningMachine<br>(Iron Ore)"]
    Screw["x3.6 Constructor<br>(Screw)"]
    IronRod["x2.4 Constructor<br>(Iron Rod)"]
    ReinforcedIronPlate_Item[12 Reinforced Iron Plate]
    IronPlate--"Iron Plate<br>(72 units/min)"-->ReinforcedIronPlate
    Screw--"Screw<br>(144 units/min)"-->ReinforcedIronPlate
    IronIngot--"Iron Ingot<br>(108 units/min)"-->IronPlate
    IronOre--"Iron Ore<br>(144 units/min)"-->IronIngot
    IronRod--"Iron Rod<br>(36 units/min)"-->Screw
    IronIngot--"Iron Ingot<br>(36 units/min)"-->IronRod
    ReinforcedIronPlate--"Reinforced Iron Plate<br>(12 units/min)"-->ReinforcedIronPlate_Item
```

<!--This is very rough. At the very beginning of the game - it looks like this:

![image](https://user-images.githubusercontent.com/8389039/153523309-5709dcaa-d231-42e9-a54c-e55a465884af.png)

After researching some basic items - it's interesting to see what the requirements are to build you initial hub/mall:
![image](https://user-images.githubusercontent.com/8389039/153591408-d2b545e1-e6fe-4629-9a5c-b8f419837721.png)

At the end of the game, it's a bit busy and I need another visualization:
![image](https://user-images.githubusercontent.com/8389039/153523397-1b80b54a-add7-4986-b2db-5b105c0f1eb5.png)

I have various filtering, to search for specific items, various "ages" of science, and to exclude buildings. For example: This shows all items to build the Gravity Matrix (the green science cube), including alternative/rare material recipes.
![image](https://user-images.githubusercontent.com/8389039/153523841-6f092c4b-80a1-4c39-b30e-fcbe872f816e.png)
-->

Now with images and zoom:
![image](https://github.com/samsmithnz/SatisfactoryTree/assets/8389039/c6e8d852-c046-4386-8e9c-f609e0377287)


