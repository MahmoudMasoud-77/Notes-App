
let saveButton = document.querySelector('#btnSave');
let deleteButton = document.querySelector('#btnDelete');
let titleInput = document.querySelector('#title');
let descriptionInput = document.querySelector('#description');
let notesContainer = document.querySelector('#notes_container');


function clearForm(){
    titleInput.value='';
    descriptionInput.value='';
    deleteButton.classList.add('hidden');
}
function displayNotesInForm(note){
    titleInput.value=note.title;
    descriptionInput.value=note.description;
    deleteButton.classList.remove('hidden');
    deleteButton.setAttribute('data-id',note.id);
    saveButton.setAttribute('data-id',note.id);

}
function getNoteById(id){
    fetch(`http://localhost:64871/api/Notes/${id}`)
    .then(data => data.json())
    .then(response => displayNotesInForm(response));
}
function populateForm(id){
   getNoteById(id);
}

function displayNotes(notes)
{
    let allNotes ='';
    notes.forEach(note => {
        const noteElement=`
            <div class="note" data-id="${note.id}">
                    <h3>${note.title}</h3>
                    <p>${note.description}</p>
            </div>
            `;
        allNotes+=noteElement;
    });
    notesContainer.innerHTML =allNotes ;   

    //add event listener
    document.querySelectorAll('.note').forEach(note => {
        note.addEventListener('click',function(e){
            populateForm(note.dataset.id);
        })
    })

}
function getAllNotes(){
    fetch('http://localhost:64871/api/Notes')
    .then(data => data.json())
    .then(response => displayNotes(response));
}

function addNote(title,description)
{
    const body ={
        title:title,
        description:description,
        isVisible:true
    };
    fetch('http://localhost:64871/api/Notes',{
        method: 'POST',
        body: JSON.stringify(body),
        headers: {
            "content-type": "application/json"
        }
    })
    .then(data => data.json())
    .then(response => {
        clearForm();
        getAllNotes();
    });
}


getAllNotes()

function updateNote(id,title,description)
{
    const body ={
        title:title,
        description:description,
        isVisible:true
    };
    fetch(`http://localhost:64871/api/Notes/${id}`,{
        method:'PUT',
        body: JSON.stringify(body),
        headers: {
            "content-type": "application/json"
        }
    })
    .then(data => data.json())
    .then(response => {
        clearForm();
        getAllNotes();
    });
}


saveButton.addEventListener('click',function(){
    const id = saveButton.dataset.id;
    if(id)
    {
        updateNote(id,titleInput.value,descriptionInput.value);
    }else{
        addNote(titleInput.value,descriptionInput.value);
    }
});

function deleteNote(id)
{
    fetch(`http://localhost:64871/api/Notes/${id}`,{
        method: 'DELETE',
        headers: {
            "content-type": "application/json"
        }
    })
    .then(response => {
        clearForm();
        getAllNotes();
    });
}

deleteButton.addEventListener('click',function(){
    const id = deleteButton.dataset.id;

    deleteNote(id);
});