module Coordinates

let shell = 
    [ "27 Arietis"; "Aries Dark Region CL-Y d25"; "Aries Dark Region FW-W d1-78"; "Aries Dark Region GR-W d1-31"; 
      "Aries Dark Region GW-W d1-52"; "Aries Dark Region ID-R b5-0"; "Aries Dark Region IS-T b3-0"; 
      "Aries Dark Region KY-Q b5-2"; "Aries Dark Region NN-T c3-4"; "Aries Dark Region NN-T c3-5"; 
      "Aries Dark Region PI-T c3-12"; "Aries Dark Region UE-P b6-0"; "Aries Dark Region UE-P b6-1"; 
      "Aries Dark Region XK-N b7-1"; "Aries Dark Region XK-N b7-2"; "Aries Dark Region YU-O b6-2"; 
      "Aries Dark Region ZF-N b7-3"; "Arietis Sector BV-Y b1"; "Arietis Sector BV-Y b5"; "Arietis Sector EQ-Y b0"; 
      "Arietis Sector EQ-Y b1"; "Arietis Sector YJ-A c1"; "Arietis Sector YO-A b3"; "Arietis Sector YO-A b4"; 
      "Arietis Sector ZE-A d36"; "Arietis Sector ZO-A B2"; "Col 285 Sector FB-X d1-38"; "Col 285 Sector FB-X d1-40"; 
      "Col 285 Sector FB-X d1-88"; "Col 285 Sector FB-X d1-89"; "Col 285 Sector GW-U b3-0"; "Col 285 Sector GW-V c2-10"; 
      "Col 285 Sector GW-V c2-11"; "Col 285 Sector GX-V c21"; "Col 285 Sector LH-Q a7-0"; "Col 285 Sector LM-Q a7-0"; 
      "Col 285 Sector MH-Q a7-1"; "Col 285 Sector MH-T b4-6"; "Col 285 Sector ON-O a8-0"; "Col 285 Sector ON-O a8-3"; 
      "Col 285 Sector ON-R b5-0"; "Col 285 Sector ON-R b5-1"; "Col 285 Sector PN-R b5-5"; "Col 285 Sector RD-R b5-1"; 
      "Col 285 Sector RN-O a8-0"; "Col 285 Sector SD-R b5-4"; "Col 285 Sector UT-M a9-0"; "Col 285 Sector VO-P b6-4"; 
      "Col 285 Sector YO-Z c20"; "Hind Sector AM-L b8-1"; "Hind Sector DH-L b8-1"; "Hind Sector FW-W d1-19"; 
      "Hind Sector JS-T c3-3"; "Hind Sector YQ-L b8-0"; "Hind Sector ZQ-L b8-0"; "HIP 10454"; "HIP 11075"; "HIP 12004"; 
      "HIP 12278"; "HIP 12985"; "HIP 13175"; "HIP 13797"; "HIP 14479"; "HIP 14621"; "HIP 18201"; "HIP 19068"; 
      "HIP 20371"; "HIP 20926"; "HIP 21155"; "HIP 21321"; "HIP 22347"; "HIP 23205"; "HIP 23597"; "HR 1517"; "HR 1528"; 
      "Hyades Sector BQ-Y d64"; "Hyades Sector BQ-Y d89"; "Hyades Sector BV-Y c13"; "Hyades Sector BV-Y c14"; 
      "Hyades Sector BV-Y c6"; "Hyades Sector BW-V b2-3"; "Hyades Sector CW-V b2-0"; "Hyades Sector DB-X c1-17"; 
      "Hyades Sector DB-X c1-19"; "Hyades Sector DL-Y d39"; "Hyades Sector DL-Y d44"; "Hyades Sector DL-Y d77"; 
      "Hyades Sector EW-V b2-0"; "Hyades Sector EW-V b2-4"; "Hyades Sector HG-X b1-4"; "Hyades Sector HR-V b2-3"; 
      "Hyades Sector HR-V b2-5"; "Hyades Sector HW-W c1-12"; "Hyades Sector IR-V b2-0"; "Hyades Sector KM-V b2-0"; 
      "Hyades Sector KM-V b2-1"; "Hyades Sector KM-V b2-4"; "Hyades Sector LX-T b3-0"; "Hyades Sector OT-X a2-4"; 
      "Hyades Sector XU-X b1-2"; "Hyades Sector XZ-Y c8"; "Hyades Sector YO-A c18"; "Hyades Sector YU-X b1-2"; 
      "Hyades Sector ZU-Y c15"; "Hyades Sector ZU-Y c6"; "Mel 22 Sector AV-P c5-4"; "Mel 22 Sector DB-O c6-7"; 
      "Mel 22 Sector DB-X d1-12"; "Mel 22 Sector DB-X D1-35"; "Mel 22 Sector DB-X d1-36"; "Mel 22 Sector DB-X d1-37"; 
      "Mel 22 Sector DB-X D1-38"; "Mel 22 Sector DB-X d1-40"; "Mel 22 Sector GH-C b13-0"; "Mel 22 Sector HC-U C3-11"; 
      "Mel 22 Sector HC-U C3-5"; "Mel 22 Sector HC-U C3-8"; "Mel 22 Sector HO-G b11-1"; "Mel 22 Sector JX-T C3-3"; 
      "Mel 22 Sector JX-T c3-9"; "Mel 22 Sector LN-T d3-65"; "Mel 22 sector OO-Q c5-12"; "Mel 22 Sector OP-E B12-0"; 
      "Mel 22 Sector PZ-N b7-2"; "Mel 22 Sector YP-F b11-0"; "Mel 22 Sector ZA-E b12-1"; "Pleiades Sector YF-N B7-2"; 
      "sssssssss"; "Struve's Lost Sector DG-0 b6-0"; "Struve's Lost Sector DG-0 b6-1"; "Struve's Lost Sector EG-O b6-1"; 
      "Struve's Lost Sector LD-S b4-1"; "Struve's Lost Sector NY-R b4-2"; "Struve's Lost Sector RE-Q b5-0"; 
      "Synuefai OZ-Z c16-17"; "Synuefai UZ-M d8-57"; "Synuefai YP-N a61-1"; "Synuefe FO-O D7-41"; "Synuefe KI-Z b33-2"; 
      "Synuefe OO-X B34-0"; "Synuefe QZ-V B35-4"; "Synuefe RT-Z c16-10"; "Taurus Dark Region CG-X c1-7"; 
      "Taurus Dark Region DB-X d1-26"; "Taurus Dark Region DL-Y d15"; "Taurus Dark Region DL-Y d16"; 
      "Taurus Dark Region DL-Y d48"; "Taurus Dark Region EL-Y d32"; "Taurus Dark Region GH-U b3-2"; 
      "Taurus Dark Region GO-E a13-1"; "Taurus Dark Region JH-V b2-0"; "Taurus Dark Region KH-V b2-1"; 
      "Taurus Dark Region MC-V b2-0"; "Taurus Dark Region MH-V b2-2"; "Taurus Dark Region OJ-P b6-1"; 
      "Taurus Dark Region OX-U b2-0"; "Taurus Dark Region PJ-P b6-0"; "Taurus Dark Region PS-U b2-2"; 
      "Taurus Dark Region SN-T b3-1"; "Taurus Dark Region UI-T b3-2"; "Taurus Dark Region YO-R b4-1"; "Wolf 221"; 
      "Wredguia VB-W c15-4"; "Wregoe LH-Q b32-4"; "Wregoe ON-O b33-3" ]
