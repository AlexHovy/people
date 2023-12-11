import React, { useState, useEffect } from "react";
import "./PersonForm.css";
import { PersonDto } from "../../../dtos/PersonDto";
import Input from "../../../components/Input/Input";
import TextArea from "../../../components/TextArea/TextArea";
import Button from "../../../components/Button/Button";

interface PersonFormProps {
  initialValues?: PersonDto | undefined;
  onSubmit: (person: PersonDto) => void;
}

const PersonForm: React.FC<PersonFormProps> = ({ initialValues, onSubmit }) => {
  const defaultPerson = {
    name: "",
    description: "",
  } as PersonDto;
  const [person, setPerson] = useState<PersonDto>(defaultPerson);

  useEffect(() => {
    setPerson(initialValues || defaultPerson);
  }, [initialValues]);

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    setPerson({ ...person, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (person) onSubmit(person);
  };

  return (
    <form onSubmit={handleSubmit}>
      <Input
        name="name"
        value={person.name}
        onChange={handleChange}
        placeholder="Name"
        required
      />
      <TextArea
        name="description"
        value={person.description ?? ""}
        onChange={handleChange}
        placeholder="Description"
      />
      <Button type="submit">Save</Button>
    </form>
  );
};

export default PersonForm;
