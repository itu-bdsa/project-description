week 1)<br>
F1: The System should be able to generate a output given any path to a local Git repository. <br>
F2: The User should be able run the program in either "commit frequency" and "commit author" mode <br>
F3: In "commit frequency" mode, the system should show the number of commits made by any author, for each day a commit was made, for the given repository. <br>
F4: In "commit author" mode, the system should show each author who has made a commit, and the number of commits they have made each day, for the given repository. <br>
F5: Dates should be shown in the yyyy-mm-dd format. (subject to change)<br>
F6: Dates should be shown in chronological order. (subject to change) <br>
F7: in "commit author" mode, the authors should be shown in alfabetical order. (subject to change) <br>

week 2)<br>
1) When running a quary on the system, it should save all the commit entries read during the quiry, in a database. <br>
    1a) The database will have 2 tables.<br>
        1a.1) first table will have link the repo path with the ID of the newest commit. <br>
        1a.2) second table will have a entry for every auther that has made a commit for a specefic date on a specefic repo that has been read by the system (I.e. the combination of repo, author and date is the key). Each commit entry will include "repo name, Auther, Date, nr of commits". <br>
2) If a quary with a repo that is already in the database, the system should check whether any new commits have been made to the repo since it was added to the database.<br>
   2a) If the repo has not have had any new commits since last the quiry was run on that repo, then it will just read all entries with that repo path generate the appropiate output based on these entries in the databse.<br>
   2b) If the repo has had new commits since the last the quiry was run on that repo, the first thing the system should do, is add all the new entries that has been made to the database. Afterwards it will enerate the appropiate output based on these entries in the database.<br>