| defaultTTFont fontsDir fstream zip |

defaultTTFont := 'komatuna-fonts-20101113'.
fontsDir := FileDirectory default / 'fonts'.

"Extract font archive"
CurrentJEISarInstaller zip extractMemberWithoutPath: (defaultTTFont, '.zip') inDirectory: fontsDir.
fstream := fontsDir readOnlyFileNamed: (defaultTTFont, '.zip').
zip := ZipArchive new readFrom: fstream.
zip extractAllTo: fontsDir.

"Copy TTF files to fonts dir"
(zip membersMatching: '*.ttf') do: [:each |
 each extractToFileNamed: each splitFileName last inDirectory: fontsDir.
].

"Clean up"
fstream close.
fontsDir deleteFileNamed: (defaultTTFont, '.zip').

!