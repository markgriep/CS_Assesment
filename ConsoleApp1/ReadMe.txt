

This is an MVP of the request.  Version 1.0 without much error checking



1. Takes two inputs and a flag
  a. A directory that contains the files to be analyzed
  b. A path for the output file (including file name and extension)
  c. A flag to determine whether or not to include subdirectories contained (and all subsequently embedded subdirectories) within the input directory ([a.] above)

2. Processes each of the files in the directory (and subdirectories if the flag is present)

3. Determines using a file signature if a given file is a PDF or a JPG
  a. JPG files start with 0x FF D8
  b. PDF files start with 0x 25 50 44 46

4. For each file that is a PDF or a JPG, creates an entry in the output CSV containing the following information
  a. The full path to the file
  b. The actual file type (PDF or JPG)
  c. The MD5 hash of the file contents
 


 NOTES:


 HEX

 JPG...
 ff=255
 d8=216
 
 PDF...
25 = 37
50 = 80
44 = 86
46 = 70
