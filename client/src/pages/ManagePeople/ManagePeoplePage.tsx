import React, { useEffect, useState } from "react";
import "./ManagePeoplePage.css";
import { PersonService } from "../../services/PersonService";
import { PersonDto } from "../../dtos/PersonDto";
import Button from "../../components/Button/Button";
import Dialog from "../../components/Dialog/Dialog";
import PersonForm from "./Form/ManagePersonForm";
import Table from "../../components/Table/Table";
import { getGenderDisplayName } from "../../constants/Gender";

const ManagePersonPage: React.FC = () => {
  const personService = new PersonService();

  const [isDeleteDialogOpen, setDeleteDialogOpen] = useState(false);

  const [isEditDialogOpen, setEditDialogOpen] = useState(false);
  const [editingPerson, setEditingPerson] = useState<PersonDto | undefined>(
    undefined
  );
  const [people, setPeople] = useState<PersonDto[]>([]);

  const columns = [
    {
      title: "Name",
      render: (person: PersonDto) => person.name,
    },
    {
      title: "Surname",
      render: (person: PersonDto) => person.surname,
    },
    {
      title: "Gender",
      render: (person: PersonDto) => getGenderDisplayName(person.gender),
    },
    {
      title: "Email",
      render: (person: PersonDto) => person.email,
    },
    {
      title: "Number",
      render: (person: PersonDto) => person.mobileNumber,
    },
    {
      title: "Country",
      render: (person: PersonDto) => person.country,
    },
    {
      title: "City",
      render: (person: PersonDto) => person.city,
    },
  ];

  useEffect(() => {
    loadPeople();
  }, []);

  const handleEdit = (person: PersonDto | undefined) => {
    setEditingPerson(person);
    setEditDialogOpen(true);
  };

  const handleDialogClose = () => {
    setEditDialogOpen(false);
    setDeleteDialogOpen(false);
    setEditingPerson(undefined);
  };

  const loadPeople = async () => {
    const data = await personService.get();
    setPeople(data);
  };

  const handleCreate = async (person: PersonDto) => {
    let newPerson = await personService.create(person);
    if (newPerson) {
      newPerson.country = person.country;
      newPerson.city = person.city;
      setPeople([...people, newPerson]);
    }
  };

  const handleUpdate = async (person: PersonDto) => {
    const updatedPerson = await personService.update(person);
    if (updatedPerson) {
      updatedPerson.country = person.country;
      updatedPerson.city = person.city;
      setPeople(
        people.map((c) => (c.id === updatedPerson.id ? updatedPerson : c))
      );
    }
  };

  const handleConfirmDelete = async (person: PersonDto) => {
    setEditingPerson(person);
    setDeleteDialogOpen(true);
  };

  const handleDelete = async () => {
    if (!editingPerson) return;

    await personService.delete(editingPerson.id);
    setPeople(people.filter((c) => c.id !== editingPerson.id));

    handleDialogClose();
  };

  const handleSubmit = async (person: PersonDto) => {
    if (editingPerson) handleUpdate(person);
    else handleCreate(person);

    handleDialogClose();
  };

  return (
    <div>
      <h1>Person Management</h1>
      <Button onClick={() => handleEdit(undefined)}>Add New Person</Button>
      <Dialog
        title={editingPerson ? "Edit Person" : "Add Person"}
        open={isEditDialogOpen}
        onClose={handleDialogClose}
      >
        <PersonForm initialValues={editingPerson} onSubmit={handleSubmit} />
      </Dialog>
      <Dialog
        title={`Confirm Removing ${editingPerson?.name}`}
        open={isDeleteDialogOpen}
        onClose={handleDialogClose}
      >
        <label>Are you sure you want to delete <strong>{editingPerson?.name} {editingPerson?.surname} ({editingPerson?.email})?</strong></label>
        <Button onClick={handleDelete}>Delete</Button>
      </Dialog>
      <Table
        data={people}
        columns={columns}
        onUpdate={handleEdit}
        onDelete={handleConfirmDelete}
      />
    </div>
  );
};

export default ManagePersonPage;
