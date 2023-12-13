import React, { useEffect, useState } from "react";
import "./PersonPage.css";
import { PersonService } from "../../services/PersonService";
import { PersonDto } from "../../dtos/PersonDto";
import Button from "../../components/Button/Button";
import Dialog from "../../components/Dialog/Dialog";
import PersonForm from "./Form/PersonForm";
import Table from "../../components/Table/Table";
import { getGenderDisplayName } from "../../constants/Gender";

const PersonPage: React.FC = () => {
  const personService = new PersonService();

  const [isDialogOpen, setDialogOpen] = useState(false);
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
  ];

  useEffect(() => {
    loadPeople();
  }, []);

  const handleEdit = (person: PersonDto | undefined) => {
    setEditingPerson(person);
    setDialogOpen(true);
  };

  const handleDialogClose = () => {
    setDialogOpen(false);
    setEditingPerson(undefined);
  };

  const loadPeople = async () => {
    const data = await personService.get();
    setPeople(data);
  };

  const handleCreate = async (person: PersonDto) => {
    const newPerson = await personService.create(person);
    if (newPerson) setPeople([...people, newPerson]);
  };

  const handleUpdate = async (person: PersonDto) => {
    const updatedPerson = await personService.update(person);
    if (updatedPerson) {
      setPeople(
        people.map((c) => (c.id === updatedPerson.id ? updatedPerson : c))
      );
    }
  };

  const handleDelete = async (person: PersonDto) => {
    await personService.delete(person.id);
    setPeople(people.filter((c) => c.id !== person.id));
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
        open={isDialogOpen}
        onClose={handleDialogClose}
      >
        <PersonForm initialValues={editingPerson} onSubmit={handleSubmit} />
      </Dialog>
      <Table
        data={people}
        columns={columns}
        onUpdate={handleEdit}
        onDelete={handleDelete}
      />
    </div>
  );
};

export default PersonPage;
