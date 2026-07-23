'From Squeak5.3 of 28 May 2021 [latest update: #19459] on 23 July 2026 at 2:39:17 pm'!

!Preferences class methodsFor: 'prefs - fonts' stamp: 'mu 7/23/2026 17:03'!
setDefaultFonts: defaultFontsSpec
	"Since this is called from menus, we can take the opportunity to prompt for missing font styles."

	| fontNames map emphases |
	fontNames := defaultFontsSpec collect: [:array | array second].
	map := IdentityDictionary new.
	emphases := IdentityDictionary new.
	fontNames do: [:originalName | | decoded style |
		decoded := TextStyle decodeStyleName: originalName.
		style := map at: originalName put: (TextStyle named: decoded second).
		emphases at: originalName put: decoded first.
		style ifNil: [map at: originalName put: TextStyle default]].

	UserInterfaceTheme current applyAfter: [
		defaultFontsSpec do: [:triplet | self
			perform: triplet first
			with: (((map at: triplet second) fontOfPointSize: triplet third) emphasized: (emphases at: triplet second))]].! !
