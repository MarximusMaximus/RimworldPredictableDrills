# v1.0.1 - Hotfix
**Changes**
- List Harmony as a dependency

# v1.0.0 - Initial Release
**Features**
- Deep drills dig up the rocks they're sitting on!
  - If a drill is sitting on a patch of stone (rough or smooth) and has no deep resources under, it will always produce that kind of stone chunk
- Only natural stone types for your map produce their own kind of chunks.
  - Meteors can leave patches of other stones, but you can't exploit them for infinite stone chunks.  Any terrain that's not a natural stone type present on your map will produce a random stone chunk selected from your map's natural stone types, just like in vanilla.
- Drills in sea ice don't produce stone chunks under any circumstance, just like in vanilla.
