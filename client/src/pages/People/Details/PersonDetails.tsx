import React, { useState, useEffect } from "react";
import "./PersonDetails.css";
import { PersonDto } from "../../../dtos/PersonDto";
import { Gender, getGenderDisplayName } from "../../../constants/Gender";

interface PersonDetailsProps {
  initialValues?: PersonDto | undefined;
}

const PersonDetails: React.FC<PersonDetailsProps> = ({ initialValues }) => {
  const defaultPerson = {
    name: "",
    surname: "",
    gender: Gender.Other,
    email: "",
    mobileNumber: "",
    countryId: "",
    cityId: "",
    profilePicture: "",
  } as PersonDto;
  const [person, setPerson] = useState<PersonDto>(defaultPerson);

  useEffect(() => {
    setPerson(initialValues || defaultPerson);
  }, [initialValues]);

  return (
    <div key={person.id} className="person-container">
      <p>
        <strong>Name:</strong> {person.name}
      </p>
      <p>
        <strong>Surname:</strong> {person.surname}
      </p>
      <p>
        <strong>Gender:</strong> {getGenderDisplayName(person.gender)}
      </p>
      <p>
        <strong>Email:</strong> {person.email}
      </p>
      <p>
        <strong>Mobile Number:</strong> {person.mobileNumber}
      </p>
      <p>
        <strong>Country:</strong> {person.country}
      </p>
      <p>
        <strong>City:</strong> {person.city}
      </p>
      <img
        className="profile-picture"
        src={person.profilePicture}
        alt={`${person.name}'s Picture`}
      />
    </div>
  );
};

export default PersonDetails;
