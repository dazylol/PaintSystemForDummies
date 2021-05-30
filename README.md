# PSFD
**Instructions**

Add the "Paintable" component to a Gameobject you wish the mouse to be over while painting, anything with collisions.

Give the component (at most) 4 materials. Starting from zero, each material index means;
- [0] - Default (Unpainted or Rusty)
- [1] - Gloss / Regular
- [2] - Metallic 
- [3] - Matte / not shiny

You can do this either with script or in the unity inspector. 

Inspector Example (Image); 

![materialSetup](https://cdn.discordapp.com/attachments/690680866304819289/848006349908606997/unknown.png)

Script example;
```cs
Paintable paintThingy = gameObject.AddComponent<Paintable>();

paintThingy.paintTypes = new Material[]
                 {
                    ATLASMOTOR,
                    Material.Instantiate(Gloss),
                    Material.Instantiate(Metallic),
                    Material.Instantiate(Matte),
                   //You instantiate these from the assetbundle you load as to not interfere with colour values on other objects using PSFD
                 };
```
Once you've done that, give it a Renderer component of the object you want painted, either in the unity inspector or in script.

Inspector example; 

![exampleSetup](https://cdn.discordapp.com/attachments/690680866304819289/848005886969511966/unknown.png)

Script example;
```cs
Paintable paintThingy = gameObject.AddComponent<Paintable>();
paintThingy.rendererIndex = gameObject.GetComponent<Renderer>();
```
After doing that, you'll want to save the values somewhere right?
Thankfully this is incredibly easy, all you'll need is these two values into whatever save system you decide to use!

Example script (Using [SaveBytes](https://github.com/Horsey4/HorseyLib));
```cs
            SaveBytes.Save(saveFile, new object[]
            {
                //0
                paintThingy.PaintColor,
                //1
                paintThingy.paintType,
	 });
```
To apply the saved values you'll want to call the "ApplyPaintSave(color, type)" void

Example script (Using [SaveBytes](https://github.com/Horsey4/HorseyLib));
```cs
var saveData = SaveBytes.Load(saveFile);
paintThingy.ApplyPaintSave(((sColor)saveData[0]).get(), (int)saveData[1]);
```
