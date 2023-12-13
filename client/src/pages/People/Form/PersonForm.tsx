import React, { useState, useEffect } from "react";
import "./PersonForm.css";
import { PersonDto } from "../../../dtos/PersonDto";
import Input from "../../../components/Input/Input";
import Button from "../../../components/Button/Button";
import {
  Gender,
  genderDisplayNames,
  getGenderDisplayName,
} from "../../../constants/Gender";
import Dropdown from "../../../components/Dropdown/Dropdown";

interface PersonFormProps {
  initialValues?: PersonDto | undefined;
  onSubmit: (person: PersonDto) => void;
}

const PersonForm: React.FC<PersonFormProps> = ({ initialValues, onSubmit }) => {
  const genderOptions = [
    { key: Gender.Other, value: getGenderDisplayName(Gender.Other) },
    { key: Gender.Male, value: getGenderDisplayName(Gender.Male) },
    { key: Gender.Female, value: getGenderDisplayName(Gender.Female) },
  ];

  const defaultPerson = {
    name: "",
    surname: "",
    gender: Gender.Other,
    email: "",
    mobileNumber: "",
    addressCity: "",
    addressCountry: "",
    profilePicture: "",
  } as PersonDto;
  const [person, setPerson] = useState<PersonDto>(defaultPerson);

  const [selectedGender, setSelectedGender] = useState<Gender>(person.gender);

  useEffect(() => {
    setPerson(initialValues || defaultPerson);
    setSelectedGender(initialValues?.gender || defaultPerson.gender);
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

  const handleGenderChange = (gender: any) => {
    person.gender = parseInt(gender);
    setPerson(person);
    setSelectedGender(person.gender);
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
      <Input
        name="surname"
        value={person.surname}
        onChange={handleChange}
        placeholder="Surname"
        required
      />
      <Dropdown
        name="gender"
        options={genderOptions}
        selectedValue={selectedGender}
        onChange={handleGenderChange}
      />
      <Input
        name="email"
        type="email"
        value={person.email}
        onChange={handleChange}
        placeholder="Email"
        required
      />
      <Input
        name="mobileNumber"
        value={person.mobileNumber}
        onChange={handleChange}
        placeholder="Mobile Number"
        required
      />
      <Button type="submit">Save</Button>
    </form>
  );
};

export default PersonForm;
