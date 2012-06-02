//Maya ASCII 2011 scene
//Name: ShotGun_Shell.ma
//Last modified: Wed, Jul 27, 2011 11:32:14 AM
//Codeset: 1252
requires maya "2011";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2011";
fileInfo "version" "2011 x64";
fileInfo "cutIdentifier" "201003190311-771506";
fileInfo "osv" "Microsoft Windows 7 Ultimate Edition, 64-bit Windows 7 Service Pack 1 (Build 7601)\n";
createNode transform -s -n "persp";
	setAttr ".v" no;
	setAttr ".t" -type "double3" -0.69876531587151214 -0.075744001692995305 0.3023827540397383 ;
	setAttr ".r" -type "double3" 13.46164726913724 -66.60000000000538 359.99999999990087 ;
createNode camera -s -n "perspShape" -p "persp";
	setAttr -k off ".v" no;
	setAttr ".fl" 34.999999999999986;
	setAttr ".coi" 0.78289505888935873;
	setAttr ".imn" -type "string" "persp";
	setAttr ".den" -type "string" "persp_depth";
	setAttr ".man" -type "string" "persp_mask";
	setAttr ".tp" -type "double3" 0 0.10650960355997086 0 ;
	setAttr ".hc" -type "string" "viewSet -p %camera";
createNode transform -s -n "top";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 100.1 0 ;
	setAttr ".r" -type "double3" -89.999999999999986 0 0 ;
createNode camera -s -n "topShape" -p "top";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "top";
	setAttr ".den" -type "string" "top_depth";
	setAttr ".man" -type "string" "top_mask";
	setAttr ".hc" -type "string" "viewSet -t %camera";
	setAttr ".o" yes;
createNode transform -s -n "front";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 0 100.1 ;
createNode camera -s -n "frontShape" -p "front";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "front";
	setAttr ".den" -type "string" "front_depth";
	setAttr ".man" -type "string" "front_mask";
	setAttr ".hc" -type "string" "viewSet -f %camera";
	setAttr ".o" yes;
createNode transform -s -n "side";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 100.1 0 0 ;
	setAttr ".r" -type "double3" 0 89.999999999999986 0 ;
createNode camera -s -n "sideShape" -p "side";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "side";
	setAttr ".den" -type "string" "side_depth";
	setAttr ".man" -type "string" "side_mask";
	setAttr ".hc" -type "string" "viewSet -s %camera";
	setAttr ".o" yes;
createNode transform -n "ShotGun_Shell";
createNode mesh -n "ShotGun_Shell" -p "|ShotGun_Shell";
	addAttr -ci true -sn "mso" -ln "miShadingSamplesOverride" -min 0 -max 1 -at "bool";
	addAttr -ci true -sn "msh" -ln "miShadingSamples" -min 0 -smx 8 -at "float";
	addAttr -ci true -sn "mdo" -ln "miMaxDisplaceOverride" -min 0 -max 1 -at "bool";
	addAttr -ci true -sn "mmd" -ln "miMaxDisplace" -min 0 -smx 1 -at "float";
	setAttr -k off ".v";
	setAttr -s 6 ".iog[0].og";
	setAttr ".iog[0].og[0].gcl" -type "componentList" 1 "f[1:72]";
	setAttr ".iog[0].og[1].gcl" -type "componentList" 1 "f[0]";
	setAttr ".iog[0].og[2].gcl" -type "componentList" 1 "f[73:108]";
	setAttr ".iog[0].og[3].gcl" -type "componentList" 2 "f[1:12]" "f[25:36]";
	setAttr ".iog[0].og[4].gcl" -type "componentList" 1 "f[109:145]";
	setAttr ".iog[0].og[5].gcl" -type "componentList" 1 "f[146:157]";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 212 ".uvst[0].uvsp[0:211]" -type "float2" 0.58333331 0.63448548 
		0.5625 0.63448548 0.54166669 0.63448548 0.52083337 0.63448548 0.50000006 0.63448548 
		0.47916669 0.63448548 0.45833337 0.63448548 0.4375 0.63448548 0.41666669 0.63448548 
		0.39583334 0.63448548 0.375 0.63448548 0.60416663 0.63448548 0.58333331 0.62312675 
		0.5625 0.62312675 0.54166669 0.62312675 0.52083337 0.62312675 0.50000006 0.62312675 
		0.47916669 0.62312675 0.45833337 0.62312675 0.4375 0.62312675 0.41666669 0.62312675 
		0.39583334 0.62312675 0.62499994 0.62312675 0.375 0.62312675 0.60416663 0.62312675 
		0.62499994 0.62312675 0.58333331 0.61893201 0.5625 0.61893201 0.54166669 0.61893201 
		0.52083337 0.61893201 0.50000006 0.61893201 0.47916669 0.61893201 0.45833337 0.61893201 
		0.43750003 0.61893201 0.41666669 0.61893201 0.39583334 0.61893201 0.62499994 0.61893201 
		0.60416663 0.61893201 0.58333331 0.62312675 0.58333331 0.61893201 0.5625 0.61893207 
		0.5625 0.62312675 0.54166669 0.61893201 0.54166669 0.62312675 0.52083337 0.61893201 
		0.52083337 0.62312675 0.50000006 0.61893207 0.50000006 0.62312675 0.47916669 0.61893201 
		0.47916669 0.62312675 0.4583334 0.61893201 0.45833337 0.62312675 0.43750006 0.61893207 
		0.4375 0.62312675 0.41666669 0.61893201 0.41666669 0.62312675 0.39583334 0.61893201 
		0.37500003 0.61893201 0.39583334 0.62312675 0.62499988 0.61893201 0.62499994 0.61893207 
		0.375 0.62312675 0.60416663 0.61893201 0.62499994 0.61952138 0.60416663 0.62312675 
		0.58333325 0.61893201 0.60416663 0.61893201 0.58333331 0.61893201 0.5625 0.61893201 
		0.5625 0.61893201 0.54166669 0.61893201 0.54166669 0.61893201 0.52083337 0.61893201 
		0.52083337 0.61893201 0.50000006 0.61893201 0.50000006 0.61893201 0.47916669 0.61893201 
		0.47916669 0.61893201 0.45833337 0.61893201 0.45833337 0.61893201 0.43750003 0.61893201 
		0.43750003 0.61893201 0.41666669 0.61893201 0.41666669 0.61893201 0.39583334 0.61893201 
		0.39583334 0.61893201 0.375 0.61893201 0.375 0.61893201 0.62499994 0.61893201 0.60416663 
		0.61893201 0.58333331 0.61952138 0.58371919 0.61893201 0.60378069 0.61893201 0.58294737 
		0.61893195 0.5625 0.61952138 0.56288594 0.61893201 0.56211406 0.61893201 0.54166669 
		0.61952138 0.54205263 0.61893201 0.54128075 0.61893201 0.52083337 0.61952138 0.52121931 
		0.61893201 0.52044743 0.61893195 0.50000006 0.61952138 0.500386 0.61893201 0.49961415 
		0.61893201 0.47916669 0.61952138 0.4795526 0.61893201 0.47878075 0.61893201 0.45833337 
		0.61952138 0.45871928 0.61893201 0.45794743 0.61893195 0.43750003 0.61952138 0.43788594 
		0.61893201 0.43711409 0.61893201 0.41666669 0.61952138 0.41705263 0.61893201 0.41628075 
		0.61893201 0.39583334 0.61952138 0.39621928 0.61893201 0.3954474 0.61893195 0.375 
		0.61952138 0.37538594 0.61893201 0.624614 0.61893201 0.60416663 0.61952138 0.60455251 
		0.61893201 0.5625 0.61893201 0.54166669 0.61893201 0.52083337 0.61893201 0.50000006 
		0.61893201 0.47916669 0.61893201 0.45833337 0.61893201 0.43750003 0.61893201 0.41666669 
		0.61893201 0.39583334 0.61893201 0.62499994 0.61893201 0.60416663 0.61893201 0.58333331 
		0.61893201 0.56249994 0.61893201 0.54166669 0.61893201 0.5625 0.61893201 0.57775205 
		0.61893201 0.54234755 0.61893201 0.52083337 0.61893201 0.5223006 0.61893201 0.50000006 
		0.61893201 0.5024361 0.61893201 0.47916669 0.61893201 0.48288769 0.61893201 0.45833337 
		0.61893201 0.46391469 0.61893201 0.43750003 0.61893201 0.44494173 0.61893207 0.41666669 
		0.61893201 0.42712006 0.61893201 0.39583334 0.61893201 0.41197333 0.61893201 0.62499994 
		0.61893201 0.5714975 0.61893201 0.60416663 0.61893201 0.60416663 0.61893201 0.58333331 
		0.61893201 0.63531649 0.921875 0.62499994 0.63448548 0.62499988 0.68775105 0.578125 
		0.97906649 0.39583331 0.68775105 0.375 0.68775105 0.5 1 0.41666669 0.68775105 0.421875 
		0.97906649 0.43750003 0.68775105 0.36468354 0.921875 0.45833334 0.68775105 0.34375 
		0.84375 0.47916669 0.68775105 0.36468354 0.765625 0.50000006 0.68775105 0.421875 
		0.70843351 0.52083337 0.68775105 0.5 0.6875 0.54166669 0.68775105 0.578125 0.70843351 
		0.5625 0.68775105 0.63531649 0.765625 0.58333331 0.68775105 0.65625 0.84375 0.60416657 
		0.68775105 0.63531649 0.921875 0.578125 0.97906649 0.63531649 0.921875 0.65625 0.84375 
		0.578125 0.97906649 0.5 1 0.5 1 0.421875 0.97906649 0.421875 0.97906649 0.36468354 
		0.921875 0.36468354 0.921875 0.34375 0.84375 0.34375 0.84375 0.36468354 0.765625 
		0.36468354 0.765625 0.421875 0.70843351 0.421875 0.70843351 0.5 0.6875 0.5 0.6875 
		0.578125 0.70843351 0.578125 0.70843351 0.63531649 0.765625 0.63531649 0.76562506 
		0.65625 0.84375;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 168 ".vt";
	setAttr ".vt[0:165]"  0.02371111 0.21301921 5.352932e-010 0.020534428 0.21301921 
		-0.011855555 0.011855555 0.21301921 -0.020534428 -6.1524785e-010 0.21301921 -0.02371111 
		-0.011855555 0.21301921 -0.020534428 -0.020534428 0.21301921 -0.011855555 -0.02371111 
		0.21301921 5.352932e-010 -0.020534428 0.21301921 0.011855555 -0.011855555 0.21301921 
		0.020534428 -6.1524785e-010 0.21301921 0.02371111 0.011855555 0.21301921 0.020534428 
		0.020534428 0.21301921 0.011855555 0.027698666 0.047666486 0.015991807 0.015991807 
		0.047666486 0.027698666 -6.1524785e-010 0.047666486 0.031983614 -0.015991807 0.047666486 
		0.027698666 -0.027698666 0.047666486 0.015991807 -0.031983614 0.047666486 5.352932e-010 
		-0.027698666 0.047666486 -0.015991807 -0.015991807 0.047666486 -0.027698666 -6.1524785e-010 
		0.047666486 -0.031983614 0.015991807 0.047666486 -0.027698666 0.027698666 0.047666486 
		-0.015991807 0.031983614 0.047666486 5.352932e-010 0.027698666 0.012962176 0.015991807 
		0.015991807 0.012962176 0.027698666 -6.1524785e-010 0.012962176 0.031983614 -0.015991807 
		0.012962176 0.027698666 -0.027698666 0.012962176 0.015991807 -0.031983614 0.012962176 
		5.352932e-010 -0.027698666 0.012962176 -0.015991807 -0.015991807 0.012962176 -0.027698666 
		-6.1524785e-010 0.012962176 -0.031983614 0.015991807 0.012962176 -0.027698666 0.027698666 
		0.012962176 -0.015991807 0.031983614 0.012962176 5.352932e-010 0.03215868 0.0097914189 
		0.018566789 0.018566789 0.0097914189 0.03215868 -6.4028427e-010 0.0097914189 0.037133597 
		-0.018566789 0.0097914189 0.032158677 -0.03215868 0.0097914189 0.018566789 -0.037133597 
		0.0097914189 5.2386895e-010 -0.03215868 0.0097914189 -0.018566789 -0.018566789 0.0097914189 
		-0.03215868 -6.4028427e-010 0.0097914189 -0.037133597 0.018566789 0.0097914189 -0.032158677 
		0.03215868 0.0097914189 -0.018566789 0.037133597 0.0097914189 5.2386895e-010 0.013175365 
		0.0065364079 0.022820437 -5.0689164e-010 0.0065364079 0.026350729 -5.1395366e-010 
		0.0065364079 0.019336555 0.0096682776 0.0065364079 0.016745975 -0.013175365 0.0065364079 
		0.022820437 -0.0096682776 0.0065364079 0.016745975 -0.022820437 0.0065364079 0.013175365 
		-0.016745975 0.0065364079 0.0096682776 -0.026350729 0.0065364079 4.410185e-010 -0.019336555 
		0.0065364079 3.709556e-010 -0.022820437 0.0065364079 -0.013175365 -0.016745975 0.0065364079 
		-0.0096682776 -0.013175365 0.0065364079 -0.022820437 -0.0096682776 0.0065364079 -0.016745975 
		-5.0689164e-010 0.0065364079 -0.026350729 -5.1395366e-010 0.0065364079 -0.019336555 
		0.013175365 0.0065364079 -0.022820437 0.0096682776 0.0065364079 -0.016745975 0.022820437 
		0.0065364079 -0.013175365 0.016745975 0.0065364079 -0.0096682776 0.026350729 0.0065364079 
		4.410185e-010 0.019336555 0.0065364079 3.709556e-010 0.022820437 0.0065364079 0.013175365 
		0.016745975 0.0065364079 0.0096682776 0.022820437 0.001375756 0.013175365 0.024011878 
		0 0.013863241 0.013175365 0.001375756 0.022820437 0.013863241 0 0.024011878 -5.0689164e-010 
		0.001375756 0.026350729 -5.2391086e-010 0 0.027726484 -0.013175365 0.001375756 0.022820437 
		-0.013863241 0 0.024011878 -0.022820437 0.001375756 0.013175365 -0.024011878 0 0.013863241 
		-0.026350729 0.001375756 4.410185e-010 -0.027726484 0 4.5158915e-010 -0.022820437 
		0.001375756 -0.013175365 -0.024011878 0 -0.013863241 -0.013175365 0.001375756 -0.022820437 
		-0.013863241 0 -0.024011878 -5.0689164e-010 0.001375756 -0.026350729 -5.2391086e-010 
		0 -0.027726484 0.013175365 0.001375756 -0.022820437 0.013863241 0 -0.024011878 0.022820437 
		0.001375756 -0.013175365 0.024011878 0 -0.013863241 0.026350729 0.001375756 4.410185e-010 
		0.027726484 0 4.5158915e-010 0.03215868 0.001375756 0.018566789 0.030967239 0 0.017878912 
		0.017878912 0 0.030967239 0.018566789 0.001375756 0.03215868 -6.2326505e-010 0 0.03575784 
		-6.4028427e-010 0.001375756 0.037133597 -0.017878912 0 0.030967236 -0.018566789 0.001375756 
		0.032158677 -0.030967239 0 0.017878912 -0.03215868 0.001375756 0.018566789 -0.03575784 
		0 5.1329829e-010 -0.037133597 0.001375756 5.2386895e-010 -0.030967239 0 -0.017878912 
		-0.03215868 0.001375756 -0.018566789 -0.017878912 0 -0.030967239 -0.018566789 0.001375756 
		-0.03215868 -6.2326505e-010 0 -0.03575784 -6.4028427e-010 0.001375756 -0.037133597 
		0.017878912 0 -0.030967236 0.018566789 0.001375756 -0.032158677 0.030967239 0 -0.017878912 
		0.03215868 0.001375756 -0.018566789 0.03575784 0 5.1329829e-010 0.037133597 0.001375756 
		5.2386895e-010 0.0096682776 0.0016679245 0.016745975 0.0088048978 0 0.015250553 -5.1395366e-010 
		0.0016679245 0.019336555 -4.9100574e-010 0 0.017609794 -0.0096682776 0.0016679245 
		0.016745975 -0.0088048978 0 0.015250553 -0.016745975 0.0016679245 0.0096682776 -0.015250553 
		0 0.0088048978 -0.019336555 0.0016679245 3.709556e-010 -0.017609794 0 3.5439252e-010 
		-0.016745975 0.0016679245 -0.0096682776 -0.015250553 0 -0.0088048978 -0.0096682776 
		0.0016679245 -0.016745975 -0.0088048978 0 -0.015250553 -5.1395366e-010 0.0016679245 
		-0.019336555 -4.9100574e-010 0 -0.017609794 0.0096682776 0.0016679245 -0.016745975 
		0.0088048978 0 -0.015250553 0.016745975 0.0016679245 -0.0096682776 0.015250553 0 
		-0.0088048978 0.019336555 0.0016679245 3.709556e-010 0.017609794 0 3.5439252e-010 
		0.016745975 0.0016679245 0.0096682776 0.015250553 0 0.0088048978 0.027698666 0.21090828 
		-0.015991807 0.025870543 0.21301921 -0.014936346 0.015991807 0.21090828 -0.027698666 
		0.014936346 0.21301921 -0.025870543 -6.1524785e-010 0.21090828 -0.031983614 -6.1524785e-010 
		0.21301921 -0.029872682 -0.015991807 0.21090828 -0.027698666 -0.014936346 0.21301921 
		-0.025870543 -0.027698666 0.21090828 -0.015991807 -0.025870543 0.21301921 -0.014936346 
		-0.031983614 0.21090828 5.352932e-010 -0.029872682 0.21301921 5.352932e-010 -0.027698666 
		0.21090828 0.015991807 -0.025870543 0.21301921 0.014936346 -0.015991807 0.21090828 
		0.027698666 -0.014936346 0.21301921 0.025870543 -6.1524785e-010 0.21090828 0.031983614 
		-6.1524785e-010 0.21301921 0.029872682 0.015991807 0.21090828 0.027698666 0.014936346 
		0.21301921 0.025870543 0.027698666 0.21090828 0.015991807 0.025870543 0.21301921 
		0.014936346;
	setAttr ".vt[166:167]" 0.031983614 0.21090828 5.352932e-010 0.029872682 0.21301921 
		5.352932e-010;
	setAttr -s 324 ".ed";
	setAttr ".ed[0:165]"  0 1 1 1 2 1 2 3 
		1 3 4 1 4 5 1 5 6 1 6 7 
		1 7 8 1 8 9 1 9 10 1 10 11 
		1 11 0 1 12 164 1 13 162 1 12 13 
		1 14 160 1 13 14 1 15 158 1 14 15 
		1 16 156 1 15 16 1 17 154 1 16 17 
		1 18 152 1 17 18 1 19 150 1 18 19 
		1 20 148 1 19 20 1 21 146 1 20 21 
		1 22 144 1 21 22 1 23 166 1 22 23 
		1 23 12 1 24 12 1 25 13 1 24 25 
		0 26 14 1 25 26 0 27 15 1 26 27 
		0 28 16 1 27 28 0 29 17 1 28 29 
		0 30 18 1 29 30 0 31 19 1 30 31 
		0 32 20 1 31 32 0 33 21 1 32 33 
		0 34 22 1 33 34 0 35 23 1 34 35 
		0 35 24 0 24 36 1 25 37 1 36 37 
		0 26 38 1 37 38 0 27 39 1 38 39 
		0 28 40 1 39 40 0 29 41 1 40 41 
		0 30 42 1 41 42 0 31 43 1 42 43 
		0 32 44 1 43 44 0 33 45 1 44 45 
		0 34 46 1 45 46 0 35 47 1 46 47 
		0 47 36 0 48 49 0 49 50 1 51 50 
		0 48 51 1 49 52 0 52 53 1 50 53 
		0 52 54 0 54 55 1 53 55 0 54 56 
		0 56 57 1 55 57 0 56 58 0 58 59 
		1 57 59 0 58 60 0 60 61 1 59 61 
		0 60 62 0 62 63 1 61 63 0 62 64 
		0 64 65 1 63 65 0 64 66 0 66 67 
		1 65 67 0 66 68 0 68 69 1 67 69 
		0 68 70 0 70 71 1 69 71 0 70 48 
		0 71 51 0 72 70 1 73 97 1 74 48 
		1 75 98 1 76 49 1 77 100 1 78 52 
		1 79 102 1 80 54 1 81 104 1 82 56 
		1 83 106 1 84 58 1 85 108 1 86 60 
		1 87 110 1 88 62 1 89 112 1 90 64 
		1 91 114 1 92 66 1 93 116 1 94 68 
		1 95 118 1 96 36 1 99 37 1 101 38 
		1 103 39 1 105 40 1 107 41 1 109 42 
		1 111 43 1 113 44 1 115 45 1 117 46 
		1 119 47 1 73 95 0 72 74 0 75 73 
		0 74 76 0 77 75 0 76 78 0 79 77 
		0 78 80 0 81 79 0 80 82 0;
	setAttr ".ed[166:323]" 83 81 0 82 84 0 85 83 
		0 84 86 0 87 85 0 86 88 0 89 87 
		0 88 90 0 91 89 0 90 92 0 93 91 
		0 92 94 0 95 93 0 94 72 0 96 119 
		0 97 98 0 98 100 0 99 96 0 100 102 
		0 101 99 0 102 104 0 103 101 0 104 106 
		0 105 103 0 106 108 0 107 105 0 108 110 
		0 109 107 0 110 112 0 111 109 0 112 114 
		0 113 111 0 114 116 0 115 113 0 116 118 
		0 117 115 0 118 97 0 119 117 0 73 72 
		1 94 95 1 75 74 1 77 76 1 79 78 
		1 81 80 1 83 82 1 85 84 1 87 86 
		1 89 88 1 91 90 1 93 92 1 96 97 
		1 118 119 1 99 98 1 101 100 1 103 102 
		1 105 104 1 107 106 1 109 108 1 111 110 
		1 113 112 1 115 114 1 117 116 1 120 51 
		1 122 50 1 124 53 1 126 55 1 128 57 
		1 130 59 1 132 61 1 134 63 1 136 65 
		1 138 67 1 140 69 1 142 71 1 123 125 
		0 125 127 0 127 129 0 129 131 0 131 133 
		0 133 135 0 135 137 0 137 139 0 139 141 
		0 141 143 0 143 121 0 121 123 0 120 142 
		0 122 120 0 124 122 0 126 124 0 128 126 
		0 130 128 0 132 130 0 134 132 0 136 134 
		0 138 136 0 140 138 0 142 140 0 121 120 
		1 122 123 1 143 142 1 124 125 1 126 127 
		1 128 129 1 130 131 1 132 133 1 134 135 
		1 136 137 1 138 139 1 140 141 1 145 1 
		1 147 2 1 149 3 1 151 4 1 153 5 
		1 155 6 1 157 7 1 159 8 1 161 9 
		1 163 10 1 165 11 1 167 0 1 145 147 
		0 144 166 0 147 149 0 146 144 0 149 151 
		0 148 146 0 151 153 0 150 148 0 153 155 
		0 152 150 0 155 157 0 154 152 0 157 159 
		0 156 154 0 159 161 0 158 156 0 161 163 
		0 160 158 0 163 165 0 162 160 0 165 167 
		0 164 162 0 167 145 0 166 164 0 145 144 
		1 146 147 1 167 166 1 148 149 1 150 151 
		1 152 153 1 154 155 1 156 157 1 158 159 
		1 160 161 1 162 163 1 164 165 1;
	setAttr -s 158 ".fc[0:157]" -type "polyFaces" 
		f 12 0 1 2 3 4 5 6 7 8 9 
		10 11 
		mu 0 12 186 162 165 168 170 172 174 176 178 
		180 182 184 
		f 4 310 276 -1 -288 
		mu 0 4 191 188 162 186 
		f 4 288 277 -2 -277 
		mu 0 4 188 192 165 162 
		f 4 290 278 -3 -278 
		mu 0 4 192 194 168 165 
		f 4 292 279 -4 -279 
		mu 0 4 194 196 170 168 
		f 4 294 280 -5 -280 
		mu 0 4 196 198 172 170 
		f 4 296 281 -6 -281 
		mu 0 4 198 200 174 172 
		f 4 298 282 -7 -282 
		mu 0 4 200 202 176 174 
		f 4 300 283 -8 -283 
		mu 0 4 202 204 178 176 
		f 4 302 284 -9 -284 
		mu 0 4 204 206 180 178 
		f 4 304 285 -10 -285 
		mu 0 4 206 208 182 180 
		f 4 306 286 -11 -286 
		mu 0 4 208 210 184 182 
		f 4 308 287 -12 -287 
		mu 0 4 210 191 186 184 
		f 4 183 144 62 -146 
		mu 0 4 94 90 38 41 
		f 4 185 145 64 -147 
		mu 0 4 97 94 41 43 
		f 4 187 146 66 -148 
		mu 0 4 100 97 43 45 
		f 4 189 147 68 -149 
		mu 0 4 103 100 45 47 
		f 4 191 148 70 -150 
		mu 0 4 106 103 47 49 
		f 4 193 149 72 -151 
		mu 0 4 109 106 49 51 
		f 4 195 150 74 -152 
		mu 0 4 112 109 51 53 
		f 4 197 151 76 -153 
		mu 0 4 115 112 53 55 
		f 4 199 152 78 -154 
		mu 0 4 118 115 55 58 
		f 4 201 153 80 -155 
		mu 0 4 121 118 58 61 
		f 4 203 154 82 -156 
		mu 0 4 124 63 25 64 
		f 4 180 155 83 -145 
		mu 0 4 90 124 64 38 
		f 4 -15 12 309 -14 
		mu 0 4 1 0 185 183 
		f 4 -17 13 307 -16 
		mu 0 4 2 1 183 181 
		f 4 -19 15 305 -18 
		mu 0 4 3 2 181 179 
		f 4 -21 17 303 -20 
		mu 0 4 4 3 179 177 
		f 4 -23 19 301 -22 
		mu 0 4 5 4 177 175 
		f 4 -25 21 299 -24 
		mu 0 4 6 5 175 173 
		f 4 -27 23 297 -26 
		mu 0 4 7 6 173 171 
		f 4 -29 25 295 -28 
		mu 0 4 8 7 171 169 
		f 4 -31 27 293 -30 
		mu 0 4 9 8 169 166 
		f 4 -33 29 291 -32 
		mu 0 4 10 9 166 167 
		f 4 -35 31 289 -34 
		mu 0 4 11 163 164 187 
		f 4 -36 33 311 -13 
		mu 0 4 0 11 187 185 
		f 4 -39 36 14 -38 
		mu 0 4 13 12 0 1 
		f 4 -41 37 16 -40 
		mu 0 4 14 13 1 2 
		f 4 -43 39 18 -42 
		mu 0 4 15 14 2 3 
		f 4 -45 41 20 -44 
		mu 0 4 16 15 3 4 
		f 4 -47 43 22 -46 
		mu 0 4 17 16 4 5 
		f 4 -49 45 24 -48 
		mu 0 4 18 17 5 6 
		f 4 -51 47 26 -50 
		mu 0 4 19 18 6 7 
		f 4 -53 49 28 -52 
		mu 0 4 20 19 7 8 
		f 4 -55 51 30 -54 
		mu 0 4 21 20 8 9 
		f 4 -57 53 32 -56 
		mu 0 4 23 21 9 10 
		f 4 -59 55 34 -58 
		mu 0 4 24 22 163 11 
		f 4 -60 57 35 -37 
		mu 0 4 12 24 11 0 
		f 4 158 121 181 -124 
		mu 0 4 68 65 39 40 
		f 4 38 61 -63 -61 
		mu 0 4 12 13 41 38 
		f 4 160 123 182 -126 
		mu 0 4 70 68 40 42 
		f 4 40 63 -65 -62 
		mu 0 4 13 14 43 41 
		f 4 162 125 184 -128 
		mu 0 4 72 70 42 44 
		f 4 42 65 -67 -64 
		mu 0 4 14 15 45 43 
		f 4 164 127 186 -130 
		mu 0 4 74 72 44 46 
		f 4 44 67 -69 -66 
		mu 0 4 15 16 47 45 
		f 4 166 129 188 -132 
		mu 0 4 76 74 46 48 
		f 4 46 69 -71 -68 
		mu 0 4 16 17 49 47 
		f 4 168 131 190 -134 
		mu 0 4 78 76 48 50 
		f 4 48 71 -73 -70 
		mu 0 4 17 18 51 49 
		f 4 170 133 192 -136 
		mu 0 4 80 78 50 52 
		f 4 50 73 -75 -72 
		mu 0 4 18 19 53 51 
		f 4 172 135 194 -138 
		mu 0 4 82 80 52 54 
		f 4 52 75 -77 -74 
		mu 0 4 19 20 55 53 
		f 4 174 137 196 -140 
		mu 0 4 84 82 54 56 
		f 4 54 77 -79 -76 
		mu 0 4 20 21 58 55 
		f 4 176 139 198 -142 
		mu 0 4 86 84 56 57 
		f 4 56 79 -81 -78 
		mu 0 4 21 23 61 58 
		f 4 178 141 200 -144 
		mu 0 4 89 59 60 62 
		f 4 58 81 -83 -80 
		mu 0 4 22 24 64 25 
		f 4 156 143 202 -122 
		mu 0 4 65 89 62 39 
		f 4 59 60 -84 -82 
		mu 0 4 24 12 38 64 
		f 4 84 85 -87 -88 
		mu 0 4 27 28 127 126 
		f 4 88 89 -91 -86 
		mu 0 4 28 29 128 127 
		f 4 91 92 -94 -90 
		mu 0 4 29 30 129 128 
		f 4 94 95 -97 -93 
		mu 0 4 30 31 130 129 
		f 4 97 98 -100 -96 
		mu 0 4 31 32 131 130 
		f 4 100 101 -103 -99 
		mu 0 4 32 33 132 131 
		f 4 103 104 -106 -102 
		mu 0 4 33 34 133 132 
		f 4 106 107 -109 -105 
		mu 0 4 34 35 134 133 
		f 4 109 110 -112 -108 
		mu 0 4 35 36 135 134 
		f 4 112 113 -115 -111 
		mu 0 4 36 37 136 135 
		f 4 115 116 -118 -114 
		mu 0 4 37 26 137 136 
		f 4 118 87 -120 -117 
		mu 0 4 26 27 126 137 
		f 4 159 124 -85 -123 
		mu 0 4 69 71 28 27 
		f 4 253 228 86 -230 
		mu 0 4 139 140 126 127 
		f 4 161 126 -89 -125 
		mu 0 4 71 73 29 28 
		f 4 254 229 90 -231 
		mu 0 4 143 139 127 128 
		f 4 163 128 -92 -127 
		mu 0 4 73 75 30 29 
		f 4 255 230 93 -232 
		mu 0 4 145 143 128 129 
		f 4 165 130 -95 -129 
		mu 0 4 75 77 31 30 
		f 4 256 231 96 -233 
		mu 0 4 147 145 129 130 
		f 4 167 132 -98 -131 
		mu 0 4 77 79 32 31 
		f 4 257 232 99 -234 
		mu 0 4 149 147 130 131 
		f 4 169 134 -101 -133 
		mu 0 4 79 81 33 32 
		f 4 258 233 102 -235 
		mu 0 4 151 149 131 132 
		f 4 171 136 -104 -135 
		mu 0 4 81 83 34 33 
		f 4 259 234 105 -236 
		mu 0 4 153 151 132 133 
		f 4 173 138 -107 -137 
		mu 0 4 83 85 35 34 
		f 4 260 235 108 -237 
		mu 0 4 155 153 133 134 
		f 4 175 140 -110 -139 
		mu 0 4 85 88 36 35 
		f 4 261 236 111 -238 
		mu 0 4 157 155 134 135 
		f 4 177 142 -113 -141 
		mu 0 4 88 66 37 36 
		f 4 262 237 114 -239 
		mu 0 4 159 157 135 136 
		f 4 179 120 -116 -143 
		mu 0 4 66 67 26 37 
		f 4 263 238 117 -240 
		mu 0 4 161 159 136 137 
		f 4 157 122 -119 -121 
		mu 0 4 67 69 27 26 
		f 4 252 239 119 -229 
		mu 0 4 140 161 137 126 
		f 4 204 -180 205 -157 
		mu 0 4 65 67 66 89 
		f 4 -205 -159 206 -158 
		mu 0 4 67 65 68 69 
		f 4 -207 -161 207 -160 
		mu 0 4 69 68 70 71 
		f 4 -208 -163 208 -162 
		mu 0 4 71 70 72 73 
		f 4 -209 -165 209 -164 
		mu 0 4 73 72 74 75 
		f 4 -210 -167 210 -166 
		mu 0 4 75 74 76 77 
		f 4 -211 -169 211 -168 
		mu 0 4 77 76 78 79 
		f 4 -212 -171 212 -170 
		mu 0 4 79 78 80 81 
		f 4 -213 -173 213 -172 
		mu 0 4 81 80 82 83 
		f 4 -214 -175 214 -174 
		mu 0 4 83 82 84 85 
		f 4 -215 -177 215 -176 
		mu 0 4 85 84 86 87 
		f 4 -216 -179 -206 -178 
		mu 0 4 88 59 89 66 
		f 4 216 -203 217 -181 
		mu 0 4 90 91 92 124 
		f 4 -217 -184 218 -182 
		mu 0 4 93 90 94 95 
		f 4 -219 -186 219 -183 
		mu 0 4 96 94 97 98 
		f 4 -220 -188 220 -185 
		mu 0 4 99 97 100 101 
		f 4 -221 -190 221 -187 
		mu 0 4 102 100 103 104 
		f 4 -222 -192 222 -189 
		mu 0 4 105 103 106 107 
		f 4 -223 -194 223 -191 
		mu 0 4 108 106 109 110 
		f 4 -224 -196 224 -193 
		mu 0 4 111 109 112 113 
		f 4 -225 -198 225 -195 
		mu 0 4 114 112 115 116 
		f 4 -226 -200 226 -197 
		mu 0 4 117 115 118 119 
		f 4 -227 -202 227 -199 
		mu 0 4 120 118 121 122 
		f 4 -228 -204 -218 -201 
		mu 0 4 123 63 124 125 
		f 12 240 241 242 243 244 245 246 247 248 249 
		250 251 
		mu 0 12 142 144 146 148 150 152 154 156 158 
		160 141 138 
		f 4 264 -254 265 -252 
		mu 0 4 138 140 139 142 
		f 4 -265 -251 266 -253 
		mu 0 4 140 138 141 161 
		f 4 -266 -255 267 -241 
		mu 0 4 142 139 143 144 
		f 4 -268 -256 268 -242 
		mu 0 4 144 143 145 146 
		f 4 -269 -257 269 -243 
		mu 0 4 146 145 147 148 
		f 4 -270 -258 270 -244 
		mu 0 4 148 147 149 150 
		f 4 -271 -259 271 -245 
		mu 0 4 150 149 151 152 
		f 4 -272 -260 272 -246 
		mu 0 4 152 151 153 154 
		f 4 -273 -261 273 -247 
		mu 0 4 154 153 155 156 
		f 4 -274 -262 274 -248 
		mu 0 4 156 155 157 158 
		f 4 -275 -263 275 -249 
		mu 0 4 158 157 159 160 
		f 4 -276 -264 -267 -250 
		mu 0 4 160 159 161 141 
		f 4 312 -292 313 -289 
		mu 0 4 188 190 189 192 
		f 4 -313 -311 314 -290 
		mu 0 4 190 188 191 211 
		f 4 -314 -294 315 -291 
		mu 0 4 192 189 193 194 
		f 4 -316 -296 316 -293 
		mu 0 4 194 193 195 196 
		f 4 -317 -298 317 -295 
		mu 0 4 196 195 197 198 
		f 4 -318 -300 318 -297 
		mu 0 4 198 197 199 200 
		f 4 -319 -302 319 -299 
		mu 0 4 200 199 201 202 
		f 4 -320 -304 320 -301 
		mu 0 4 202 201 203 204 
		f 4 -321 -306 321 -303 
		mu 0 4 204 203 205 206 
		f 4 -322 -308 322 -305 
		mu 0 4 206 205 207 208 
		f 4 -323 -310 323 -307 
		mu 0 4 208 207 209 210 
		f 4 -324 -312 -315 -309 
		mu 0 4 210 209 211 191 ;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
createNode lightLinker -s -n "lightLinker1";
	setAttr -s 5 ".lnk";
	setAttr -s 5 ".slnk";
createNode displayLayerManager -n "layerManager";
createNode displayLayer -n "defaultLayer";
createNode renderLayerManager -n "renderLayerManager";
createNode renderLayer -n "defaultRenderLayer";
	setAttr ".g" yes;
createNode phong -n "phong1";
	setAttr ".c" -type "float3" 0 0 0 ;
	setAttr ".sc" -type "float3" 0 0 0 ;
createNode shadingEngine -n "phong1SG";
	setAttr ".ihi" 0;
	setAttr ".ro" yes;
createNode materialInfo -n "materialInfo1";
createNode phong -n "phong2";
	setAttr ".c" -type "float3" 0.43137255 0.29468071 0.033833139 ;
	setAttr ".sc" -type "float3" 1 0.93849885 0 ;
createNode shadingEngine -n "phong2SG";
	setAttr ".ihi" 0;
	setAttr -s 3 ".dsm";
	setAttr ".ro" yes;
	setAttr -s 3 ".gn";
createNode materialInfo -n "materialInfo2";
createNode script -n "uiConfigurationScriptNode";
	setAttr ".b" -type "string" (
		"// Maya Mel UI Configuration File.\n//\n//  This script is machine generated.  Edit at your own risk.\n//\n//\n\nglobal string $gMainPane;\nif (`paneLayout -exists $gMainPane`) {\n\n\tglobal int $gUseScenePanelConfig;\n\tint    $useSceneConfig = $gUseScenePanelConfig;\n\tint    $menusOkayInPanels = `optionVar -q allowMenusInPanels`;\tint    $nVisPanes = `paneLayout -q -nvp $gMainPane`;\n\tint    $nPanes = 0;\n\tstring $editorName;\n\tstring $panelName;\n\tstring $itemFilterName;\n\tstring $panelConfig;\n\n\t//\n\t//  get current state of the UI\n\t//\n\tsceneUIReplacement -update $gMainPane;\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Top View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"top\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"wireframe\" \n"
		+ "                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 8192\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"base_OpenGL_Renderer\" \n"
		+ "                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -joints 1\n                -ikHandles 1\n"
		+ "                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -shadows 0\n                $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"top\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n"
		+ "            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 8192\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"base_OpenGL_Renderer\" \n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n"
		+ "            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -shadows 0\n"
		+ "            $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Side View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"side\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"wireframe\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n"
		+ "                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 8192\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"base_OpenGL_Renderer\" \n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n"
		+ "                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -shadows 0\n"
		+ "                $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"side\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 8192\n"
		+ "            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"base_OpenGL_Renderer\" \n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n"
		+ "            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -shadows 0\n            $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Front View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n"
		+ "                -camera \"front\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"wireframe\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 8192\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n"
		+ "                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"base_OpenGL_Renderer\" \n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n"
		+ "                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -shadows 0\n                $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"front\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n"
		+ "            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 8192\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"base_OpenGL_Renderer\" \n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n"
		+ "            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n"
		+ "            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -shadows 0\n            $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Persp View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"persp\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n"
		+ "                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 1\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 8192\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"hwRender_OpenGL_Renderer\" \n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n"
		+ "                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n"
		+ "                -manipulators 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -shadows 0\n                $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"persp\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 1\n            -smoothWireframe 0\n"
		+ "            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 8192\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"hwRender_OpenGL_Renderer\" \n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n"
		+ "            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -shadows 0\n            $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"Outliner\")) `;\n"
		+ "\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `outlinerPanel -unParent -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            outlinerEditor -e \n                -showShapes 0\n                -showAttributes 0\n                -showConnected 0\n                -showAnimCurvesOnly 0\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 1\n                -showAssets 1\n                -showContainedOnly 1\n                -showPublishedAsConnected 0\n                -showContainerContents 1\n                -ignoreDagHierarchy 0\n                -expandConnections 0\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 0\n                -highlightActive 1\n                -autoSelectNewObjects 0\n"
		+ "                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"defaultSetFilter\" \n                -showSetMembers 1\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n"
		+ "            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"graphEditor\" (localizedPanelLabel(\"Graph Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"graphEditor\" -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAttributes 1\n"
		+ "                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n"
		+ "                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n"
		+ "                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n"
		+ "                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n"
		+ "                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n"
		+ "                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dopeSheetPanel\" (localizedPanelLabel(\"Dope Sheet\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dopeSheetPanel\" -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n"
		+ "                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n"
		+ "                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n"
		+ "                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n"
		+ "                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n"
		+ "                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"clipEditorPanel\" (localizedPanelLabel(\"Trax Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"clipEditorPanel\" -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 0 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n"
		+ "\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"sequenceEditorPanel\" (localizedPanelLabel(\"Camera Sequencer\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"sequenceEditorPanel\" -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n"
		+ "                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperGraphPanel\" (localizedPanelLabel(\"Hypergraph Hierarchy\")) `;\n"
		+ "\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperGraphPanel\" -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n"
		+ "                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n"
		+ "                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperShadePanel\" (localizedPanelLabel(\"Hypershade\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperShadePanel\" -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\tif ($useSceneConfig) {\n\t\tscriptedPanel -e -to $panelName;\n"
		+ "\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"visorPanel\" (localizedPanelLabel(\"Visor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"visorPanel\" -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"polyTexturePlacementPanel\" (localizedPanelLabel(\"UV Texture Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"polyTexturePlacementPanel\" -l (localizedPanelLabel(\"UV Texture Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"UV Texture Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"renderWindowPanel\" (localizedPanelLabel(\"Render View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"renderWindowPanel\" -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"blendShapePanel\" (localizedPanelLabel(\"Blend Shape\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\tblendShapePanel -unParent -l (localizedPanelLabel(\"Blend Shape\")) -mbv $menusOkayInPanels ;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tblendShapePanel -edit -l (localizedPanelLabel(\"Blend Shape\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynRelEdPanel\" (localizedPanelLabel(\"Dynamic Relationships\")) `;\n"
		+ "\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynRelEdPanel\" -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"relationshipPanel\" (localizedPanelLabel(\"Relationship Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"relationshipPanel\" -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"referenceEditorPanel\" (localizedPanelLabel(\"Reference Editor\")) `;\n"
		+ "\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"referenceEditorPanel\" -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"componentEditorPanel\" (localizedPanelLabel(\"Component Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"componentEditorPanel\" -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynPaintScriptedPanelType\" (localizedPanelLabel(\"Paint Effects\")) `;\n"
		+ "\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynPaintScriptedPanelType\" -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"scriptEditorPanel\" (localizedPanelLabel(\"Script Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"scriptEditorPanel\" -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\tif ($useSceneConfig) {\n        string $configName = `getPanel -cwl (localizedPanelLabel(\"Current Layout\"))`;\n"
		+ "        if (\"\" != $configName) {\n\t\t\tpanelConfiguration -edit -label (localizedPanelLabel(\"Current Layout\")) \n\t\t\t\t-defaultImage \"\"\n\t\t\t\t-image \"\"\n\t\t\t\t-sc false\n\t\t\t\t-configString \"global string $gMainPane; paneLayout -e -cn \\\"vertical2\\\" -ps 1 22 100 -ps 2 78 100 $gMainPane;\"\n\t\t\t\t-removeAllPanels\n\t\t\t\t-ap false\n\t\t\t\t\t(localizedPanelLabel(\"Outliner\")) \n\t\t\t\t\t\"outlinerPanel\"\n\t\t\t\t\t\"$panelName = `outlinerPanel -unParent -l (localizedPanelLabel(\\\"Outliner\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\noutlinerEditor -e \\n    -showShapes 0\\n    -showAttributes 0\\n    -showConnected 0\\n    -showAnimCurvesOnly 0\\n    -showMuteInfo 0\\n    -organizeByLayer 1\\n    -showAnimLayerWeight 1\\n    -autoExpandLayers 1\\n    -autoExpand 0\\n    -showDagOnly 1\\n    -showAssets 1\\n    -showContainedOnly 1\\n    -showPublishedAsConnected 0\\n    -showContainerContents 1\\n    -ignoreDagHierarchy 0\\n    -expandConnections 0\\n    -showUpstreamCurves 1\\n    -showUnitlessCurves 1\\n    -showCompounds 1\\n    -showLeafs 1\\n    -showNumericAttrsOnly 0\\n    -highlightActive 1\\n    -autoSelectNewObjects 0\\n    -doNotSelectNewObjects 0\\n    -dropIsParent 1\\n    -transmitFilters 0\\n    -setFilter \\\"defaultSetFilter\\\" \\n    -showSetMembers 1\\n    -allowMultiSelection 1\\n    -alwaysToggleSelect 0\\n    -directSelect 0\\n    -displayMode \\\"DAG\\\" \\n    -expandObjects 0\\n    -setsIgnoreFilters 1\\n    -containersIgnoreFilters 0\\n    -editAttrName 0\\n    -showAttrValues 0\\n    -highlightSecondary 0\\n    -showUVAttrsOnly 0\\n    -showTextureNodesOnly 0\\n    -attrAlphaOrder \\\"default\\\" \\n    -animLayerFilterOptions \\\"allAffecting\\\" \\n    -sortOrder \\\"none\\\" \\n    -longNames 0\\n    -niceNames 1\\n    -showNamespace 1\\n    -showPinIcons 0\\n    $editorName\"\n"
		+ "\t\t\t\t\t\"outlinerPanel -edit -l (localizedPanelLabel(\\\"Outliner\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\noutlinerEditor -e \\n    -showShapes 0\\n    -showAttributes 0\\n    -showConnected 0\\n    -showAnimCurvesOnly 0\\n    -showMuteInfo 0\\n    -organizeByLayer 1\\n    -showAnimLayerWeight 1\\n    -autoExpandLayers 1\\n    -autoExpand 0\\n    -showDagOnly 1\\n    -showAssets 1\\n    -showContainedOnly 1\\n    -showPublishedAsConnected 0\\n    -showContainerContents 1\\n    -ignoreDagHierarchy 0\\n    -expandConnections 0\\n    -showUpstreamCurves 1\\n    -showUnitlessCurves 1\\n    -showCompounds 1\\n    -showLeafs 1\\n    -showNumericAttrsOnly 0\\n    -highlightActive 1\\n    -autoSelectNewObjects 0\\n    -doNotSelectNewObjects 0\\n    -dropIsParent 1\\n    -transmitFilters 0\\n    -setFilter \\\"defaultSetFilter\\\" \\n    -showSetMembers 1\\n    -allowMultiSelection 1\\n    -alwaysToggleSelect 0\\n    -directSelect 0\\n    -displayMode \\\"DAG\\\" \\n    -expandObjects 0\\n    -setsIgnoreFilters 1\\n    -containersIgnoreFilters 0\\n    -editAttrName 0\\n    -showAttrValues 0\\n    -highlightSecondary 0\\n    -showUVAttrsOnly 0\\n    -showTextureNodesOnly 0\\n    -attrAlphaOrder \\\"default\\\" \\n    -animLayerFilterOptions \\\"allAffecting\\\" \\n    -sortOrder \\\"none\\\" \\n    -longNames 0\\n    -niceNames 1\\n    -showNamespace 1\\n    -showPinIcons 0\\n    $editorName\"\n"
		+ "\t\t\t\t-ap false\n\t\t\t\t\t(localizedPanelLabel(\"Persp View\")) \n\t\t\t\t\t\"modelPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `modelPanel -unParent -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 1\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 1\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 8192\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -maxConstantTransparency 1\\n    -rendererName \\\"hwRender_OpenGL_Renderer\\\" \\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -shadows 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName\"\n"
		+ "\t\t\t\t\t\"modelPanel -edit -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 1\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 1\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 8192\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -maxConstantTransparency 1\\n    -rendererName \\\"hwRender_OpenGL_Renderer\\\" \\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -shadows 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName\"\n"
		+ "\t\t\t\t$configName;\n\n            setNamedPanelLayout (localizedPanelLabel(\"Current Layout\"));\n        }\n\n        panelHistory -e -clear mainPanelHistory;\n        setFocus `paneLayout -q -p1 $gMainPane`;\n        sceneUIReplacement -deleteRemaining;\n        sceneUIReplacement -clear;\n\t}\n\n\ngrid -spacing 5 -size 12 -divisions 5 -displayAxes yes -displayGridLines yes -displayDivisionLines yes -displayPerspectiveLabels no -displayOrthographicLabels no -displayAxesBold yes -perspectiveLabelPosition axis -orthographicLabelPosition edge;\nviewManip -drawCompass 0 -compassAngle 0 -frontParameters \"\" -homeParameters \"\" -selectionLockParameters \"\";\n}\n");
	setAttr ".st" 3;
createNode script -n "sceneConfigurationScriptNode";
	setAttr ".b" -type "string" "playbackOptions -min 1 -max 24 -ast 1 -aet 48 ";
	setAttr ".st" 6;
createNode phong -n "phong3";
	setAttr ".dc" 0.69105690717697144;
	setAttr ".c" -type "float3" 1 0.034375012 0 ;
	setAttr ".it" -type "float3" 0.032516975 0.032516975 0.032516975 ;
	setAttr ".tcf" 0;
	setAttr ".trsd" 0;
	setAttr ".sc" -type "float3" 0.54471654 0.35934997 0.3524071 ;
	setAttr ".rfl" 0.5528455376625061;
	setAttr ".cp" 14.747967720031738;
createNode shadingEngine -n "phong3SG";
	setAttr ".ihi" 0;
	setAttr -s 2 ".dsm";
	setAttr ".ro" yes;
	setAttr -s 2 ".gn";
createNode materialInfo -n "materialInfo3";
createNode groupId -n "groupId1";
	setAttr ".ihi" 0;
createNode groupId -n "groupId2";
	setAttr ".ihi" 0;
createNode groupId -n "groupId3";
	setAttr ".ihi" 0;
createNode groupId -n "groupId4";
	setAttr ".ihi" 0;
createNode groupId -n "groupId5";
	setAttr ".ihi" 0;
createNode groupId -n "groupId6";
	setAttr ".ihi" 0;
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :renderPartition;
	setAttr -s 5 ".st";
select -ne :initialShadingGroup;
	setAttr ".ro" yes;
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :defaultShaderList1;
	setAttr -s 5 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :renderGlobalsList1;
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
select -ne :defaultHardwareRenderGlobals;
	setAttr ".fn" -type "string" "im";
	setAttr ".res" -type "string" "ntsc_4d 646 485 1.333";
connectAttr "groupId1.id" "|ShotGun_Shell|ShotGun_Shell.iog.og[0].gid";
connectAttr "phong2SG.mwc" "|ShotGun_Shell|ShotGun_Shell.iog.og[0].gco";
connectAttr "groupId2.id" "|ShotGun_Shell|ShotGun_Shell.iog.og[1].gid";
connectAttr "phong1SG.mwc" "|ShotGun_Shell|ShotGun_Shell.iog.og[1].gco";
connectAttr "groupId3.id" "|ShotGun_Shell|ShotGun_Shell.iog.og[2].gid";
connectAttr "phong2SG.mwc" "|ShotGun_Shell|ShotGun_Shell.iog.og[2].gco";
connectAttr "groupId4.id" "|ShotGun_Shell|ShotGun_Shell.iog.og[3].gid";
connectAttr "phong3SG.mwc" "|ShotGun_Shell|ShotGun_Shell.iog.og[3].gco";
connectAttr "groupId5.id" "|ShotGun_Shell|ShotGun_Shell.iog.og[4].gid";
connectAttr "phong2SG.mwc" "|ShotGun_Shell|ShotGun_Shell.iog.og[4].gco";
connectAttr "groupId6.id" "|ShotGun_Shell|ShotGun_Shell.iog.og[5].gid";
connectAttr "phong3SG.mwc" "|ShotGun_Shell|ShotGun_Shell.iog.og[5].gco";
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "phong1SG.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "phong2SG.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "phong3SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "phong1SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "phong2SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "phong3SG.message" ":defaultLightSet.message";
connectAttr "layerManager.dli[0]" "defaultLayer.id";
connectAttr "renderLayerManager.rlmi[0]" "defaultRenderLayer.rlid";
connectAttr "phong1.oc" "phong1SG.ss";
connectAttr "groupId2.msg" "phong1SG.gn" -na;
connectAttr "|ShotGun_Shell|ShotGun_Shell.iog.og[1]" "phong1SG.dsm" -na;
connectAttr "phong1SG.msg" "materialInfo1.sg";
connectAttr "phong1.msg" "materialInfo1.m";
connectAttr "phong2.oc" "phong2SG.ss";
connectAttr "|ShotGun_Shell|ShotGun_Shell.iog.og[0]" "phong2SG.dsm" -na;
connectAttr "|ShotGun_Shell|ShotGun_Shell.iog.og[2]" "phong2SG.dsm" -na;
connectAttr "|ShotGun_Shell|ShotGun_Shell.iog.og[4]" "phong2SG.dsm" -na;
connectAttr "groupId1.msg" "phong2SG.gn" -na;
connectAttr "groupId3.msg" "phong2SG.gn" -na;
connectAttr "groupId5.msg" "phong2SG.gn" -na;
connectAttr "phong2SG.msg" "materialInfo2.sg";
connectAttr "phong2.msg" "materialInfo2.m";
connectAttr "phong3.oc" "phong3SG.ss";
connectAttr "groupId4.msg" "phong3SG.gn" -na;
connectAttr "groupId6.msg" "phong3SG.gn" -na;
connectAttr "|ShotGun_Shell|ShotGun_Shell.iog.og[3]" "phong3SG.dsm" -na;
connectAttr "|ShotGun_Shell|ShotGun_Shell.iog.og[5]" "phong3SG.dsm" -na;
connectAttr "phong3SG.msg" "materialInfo3.sg";
connectAttr "phong3.msg" "materialInfo3.m";
connectAttr "phong1SG.pa" ":renderPartition.st" -na;
connectAttr "phong2SG.pa" ":renderPartition.st" -na;
connectAttr "phong3SG.pa" ":renderPartition.st" -na;
connectAttr "phong1.msg" ":defaultShaderList1.s" -na;
connectAttr "phong2.msg" ":defaultShaderList1.s" -na;
connectAttr "phong3.msg" ":defaultShaderList1.s" -na;
// End of ShotGun_Shell.ma
