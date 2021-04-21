# HoExtractor

A tool for extracting assets from Heavy Iron Studio's .HO archive files. Notable games that use this format are:

- Family Guy: Back to the Multiverse (2012)
- Up (2009)
- SpongeBob's Truth or Square (2009)
- WALL-E (2008)
- Ratatouille (2007)

This repo contains two projects; `HoLib` which contains the logic to process the `.ho` archives and `HoExtractor` which is a GUI application to browse the archive contents.
See Releases for a compiled Win x64 binary.

#### Usage:

1. Run `HoExtractor.exe`
2. Click `Load` and select a game directory
3. Select the archive you use to view from the list of archives
4. Highlight any assets you wish to export
5. Click `Export` and select a directory to export to

#### Notes:

Unlike regular archives, files appear to be referenced by a combination of their `AssetID`, `AssetType`, flags and/or internal name rather than a file path. Due to this a custom naming convention has been devised for exporting files which is `{archive_path}/{flags}/{internal_name}.{AssetID}.{AssetType}.{extension?}` (extension may be unavailable). This path is near-enough how the games organise the files in memory and also avoids export conflicts.

[AssetTypes](./HoLib/Static/AssetType.cs) are component names, these are subsequently DKDR hashed when added to the archive. Due to this process these names have been lost however through a combination of brute forcing and binary string dumps, a large number have been named. The remaining missing types will display as their hashed hex representation so that nothing is lost.

Flags are used to control file loading within the games. They dictate when a certain version of an asset should be used over another such as with localisation. Unfortunately I wasn't able to work out flag meanings but knowing the this means that if a certain asset is localised, all other assets with the same flags will have the same locale so can be grouped.