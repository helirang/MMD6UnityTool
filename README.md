﻿# MMD6UnityTool_Custom
## Add Function
- Mope(blendshape) animation automatic parsing
- Warning that Morph(blendshape) included in the animation is not in the model
![image](https://user-images.githubusercontent.com/66342017/223606644-6f4d2316-c698-4730-a84c-d337bfeeb54d.png) <br>

##
May be used when you use Unity to make MMD

For **export camera and morph animations** from VMD (Character animations, and pmx => fbx you can export
via [MMD4Mecanim](https://stereoarts.jp/))
<br>
For ease of use, directly copied from the following project and put it in a menu

[MMD2UnityTool](https://github.com/cnscj/MMD2UnityTool)
[MMD2UnityTool_Edit_Version](https://github.com/MorphoDiana/MMD2UnityTool)
[MMD4UnityTools](https://github.com/ShiinaRinne/MMD4UnityTools)

(2+4=6)

## Known Issue
Sometimes one eye may lose morph animation. <br>
At present, you can copy the key frame of the other eye through the Animation window( Ctrl + 6 ).
## Usage

- Unity Camera Structure <br>
![image](https://user-images.githubusercontent.com/66342017/223605229-38bd3891-3bf4-4642-8e20-cf93291213ac.png)

- For camera animation, just right click on VMD file and select `MMD/Create Camera Anim`<br>
  When you use camera animation via Timeline, please uncheck `Remove Start Offset` in Clip properties<br>
  ![image](https://user-images.githubusercontent.com/66342017/223605422-31ee9db1-be4c-4798-a2b6-b7fd22e228c3.png)
- For morph animation, you need to select the object in the scene that contains the blendshapes of the face under the
  model
  (If it is generated by MMD4Mecanim, it is usually `your model/U_Char/U_Char_1`)<br>
  and select the VMD with morph (ie: multi selection)<br>
  ![image](https://user-images.githubusercontent.com/66342017/223604895-7a4fb967-4efa-4ea2-bf91-29c35b15afd8.png) <br>
  Then right click and select `MMD/Create Morph Anim`

## License
[MIT License](https://github.com/ShiinaRinne/MMD6UnityTool/blob/master/LICENSE)

