import { useEffect, useState } from "react";
import { PersonDto } from "../../dtos/PersonDto";
import { PersonService } from "../../services/PersonService";
import "./PeoplePage.css";
import Table from "../../components/Table/Table";
import { getGenderDisplayName } from "../../constants/Gender";

function PeoplePage() {
  const personService = new PersonService();

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

  const loadPeople = async () => {
    const data = await personService.get();
    setPeople(data);
  };
  
  return (
    <div>
      <h1>People</h1>
      <Table
        data={people}
        columns={columns}
      />
    </div>
  );
}

export default PeoplePage;
