week 1)<br>
F1: The System should be able to generate a output given any path to a local Git repository. <br>
F2: The User should be able run the program in either "commit frequency" and "commit author" mode <br>
F3: In "commit frequency" mode, the system should show the number of commits made by any author, for each day a commit was made, for the given repository. <br>
F4: In "commit author" mode, the system should show each author who has made a commit, and the number of commits they have made each day, for the given repository. <br>
F5: Dates should be shown in the yyyy-mm-dd format. (subject to change)<br>
F6: Dates should be shown in chronological order. (subject to change) <br>
F7: in "commit author" mode, the authors should be shown in alfabetical order. (subject to change) <br>

week 2 (updated for week 3 changes))<br>
1) When running a quary on the system, it should save all the commit entries read during the quiry, in a database. <br>
    1a) The database will have a single table.<br>
        1a.1) The table will have the repo path as an attribute.
        1a.2) The table will have an ID of the newest commit as an attribute. <br>
        1a.3) The table will have a collection of contributions objects. Each contribution object will include "Auther, Date, nr of commits". This will be the objects the system reads to generate the appropriate output.<br>
2) If a quary with a repo that is already in the database, the system should check whether any new commits have been made to the repo since it was added to the database.<br>
    2a) if the repo path doesn't exist in the datebase, it will create an entirely new entry in the table, including the repo path, ID of last commit, and all the contribution objects for the database will be created and saved.
    2b) If the repo exists in the database, but haven't had any new commits since last the quiry was run on that repo, then it will just read all contributions objects for the given repo, and generate the appropiate output based on these objects in the database.<br>
    2c) If the repo has had new commits since the last time the quiry was run on that repo, the first thing the system should do, is add all the new entries as contribution objects and save them in the database under the given repo. Afterwards it will generate the appropiate output based on these contribution objects under the specified repo.<br>