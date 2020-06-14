--[[
File: 		 gettext_lua.lua
Description: A simple parser for Lua files to gather strings for translation
			 for the Open MTA:DayZ project. (vRoleplay)
]]

require("lfs")

function removeComments(input)
	-- Remove multiline comments
	input = input:gsub("%/%*.-%*%/", "")
	-- Remove single lined comments
	input = input:gsub("%/%/.-\n", "\n")

	return input
end

-- Processes a lua file and writes the strings to pothandle
function processFile(path, name, pothandle)
	print("   Processing file "..path.."/"..name)

	-- Read the file
	local fh = io.open(path.."/"..name, "r")
	local input = fh:read("*all")
	fh:close()
	input = removeComments(input)

	-- 	this gmatch matches T._("TEXT")
	for match in string.gmatch(input, 'T._%(%"(.-)%"') do -- .-_%(?"(.-)"%)?
		print(match)
		pothandle:write("#: "..path.."/"..name..":0\n")
		pothandle:write("msgid \""..match.."\"\n")
		pothandle:write("msgstr \"\"\n")
		pothandle:write("\n")
	end
end

-- Processes all lua files in a directory and all subdirectories
function processDirectory(dir, pothandle)
	print("\nProcessing directory "..dir.."...")
	for file in lfs.dir(dir) do
		if file == "." or file == ".." then
		else
			if lfs.attributes(dir.."/"..file).mode == "directory" then
				processDirectory(dir.."/"..file, pothandle)
			else
				processFile(dir, file, pothandle)
			end
		end
	end
end

function writePotHeader(pothandle)
	pothandle:write("# eXo Roleplay Translation File\n")
	pothandle:write("# https://exo-roleplay.de\n")
	pothandle:write("\"Project-Id-Version: PACKAGE VERSION\\n\"\n",
					"\"Report-Msgid-Bugs-To: \\n\"\n",
					"\"POT-Creation-Date: "..os.date("%Y-%m-%d %H:%M+0000").."\\n\"\n", -- Note: This ignores the local timezone :(
					"\"PO-Revision-Date: "..os.date("%Y-%m-%d %H:%M+0000").."\\n\"\n", -- Note: This ignores the local timezone :(
					"\"Last-Translator: none <none@no.mail>\\n\"\n",
					"\"Language-Team: LANGUAGE <LL@li.org>\\n\"\n",
					"\"MIME-Version: 1.0\\n\"\n",
					"\"Content-Type: text/plain; charset=UTF-8\\n\"\n",
					"\"Content-Transfer-Encoding: 8bit\\n\"\n\n")
end

function genpot(name)
	local pot = io.open(name:gsub("/", "_")..".pot", "w")
	writePotHeader(pot)

	-- change to main directory
	lfs.chdir("../")
	processDirectory(name, pot)

	-- go back to tools
	lfs.chdir("../tools")
	pot:close()
end

genpot("server/Exo.Rp.Core")
