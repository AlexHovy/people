import React, { useState, useEffect } from "react";
import "./PersonDetails.css";
import { PersonDto } from "../../../dtos/PersonDto";
import { Gender, getGenderDisplayName } from "../../../constants/Gender";
import { PersonService } from "../../../services/PersonService";
import { Base64Prefixes } from "../../../constants/Base64Prefixes";

interface PersonDetailsProps {
  initialValues?: PersonDto | undefined;
}

const PersonDetails: React.FC<PersonDetailsProps> = ({ initialValues }) => {
  const personService = new PersonService();

  const defaultPerson = {
    name: "",
    surname: "",
    gender: Gender.Other,
    email: "",
    mobileNumber: "",
    countryId: "",
    cityId: "",
    hasProfilePicture: false,
  } as PersonDto;
  const [person, setPerson] = useState<PersonDto>(defaultPerson);
  const [profilePicture, setProfilePicture] = useState<string>("");

  useEffect(() => {
    const initPerson = initialValues || defaultPerson;
    setPerson(initPerson);
    getProfilePicture(initPerson);
  }, [initialValues]);

  const getProfilePicture = (person: PersonDto) => {
    if (!person || !person.hasProfilePicture) return;

    personService.getProfilePicture(person.id).then((fileDto) => {
      if (fileDto) setProfilePicture(`${Base64Prefixes.IMAGE_PNG}${fileDto.fileBase64}`);
    });
  };

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
      {person.hasProfilePicture && (
        <img
          className="profile-picture"
          src={profilePicture}
          alt={`${person.name}'s Picture`}
        />
      )}
    </div>
  );
};

export default PersonDetails;
