import { useEffect, useState } from "react";
import { PersonDto } from "../../dtos/PersonDto";
import { PersonService } from "../../services/PersonService";
import "./PeoplePage.css";
import Table from "../../components/Table/Table";
import { getGenderDisplayName } from "../../constants/Gender";
import Input from "../../components/Input/Input";
import { CountryDto } from "../../dtos/CountryDto";
import { CityDto } from "../../dtos/CityDto";
import Dropdown from "../../components/Dropdown/Dropdown";
import { CountryService } from "../../services/CountryService";
import { CityService } from "../../services/CityService";
import Button from "../../components/Button/Button";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEye, faTrash } from "@fortawesome/free-solid-svg-icons";
import PersonDetails from "./Details/PersonDetails";
import Dialog from "../../components/Dialog/Dialog";

function PeoplePage() {
  const personService = new PersonService();
  const countryService = new CountryService();
  const cityService = new CityService();

  const [isDialogOpen, setDialogOpen] = useState(false);

  const [people, setPeople] = useState<PersonDto[]>([]);
  const [selectedPerson, setSelectedPerson] = useState<PersonDto>();
  const [filteredPeople, setFilteredPeople] = useState<PersonDto[]>([]);

  const [searchValue, setSearchValue] = useState<string>("");

  const [countries, setCountries] = useState<CountryDto[]>([]);
  const [selectedCountry, setSelectedCountry] = useState<CountryDto>();
  const [countryOptions, setCountryOptions] = useState<
    { key: any; value: string }[]
  >([{ key: "", value: "Select a country" }]);

  const [cities, setCities] = useState<CityDto[]>([]);
  const [selectedCity, setSelectedCity] = useState<CityDto>();
  const [cityOptions, setCityOptions] = useState<{ key: any; value: string }[]>(
    [{ key: "", value: "Select a city" }]
  );

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
    loadCountries();
  }, []);

  const loadPeople = async () => {
    const people = await personService.get();
    setPeople(people);
    setFilteredPeople(people);
  };

  const loadCountries = async () => {
    await countryService.get().then((countries) => {
      setCountries(countries);
      loadCountryOptions(countries);
    });
  };

  const loadCountryOptions = async (countries: CountryDto[]) => {
    const options: { key: any; value: string }[] = [
      { key: "", value: "Select a country" },
      ...countries.map((country) => ({
        key: country.id,
        value: country.name,
      })),
    ];
    setCountryOptions(options);
  };

  const loadCities = async (countryId: string) => {
    await cityService.getByCountryId(countryId).then((cities) => {
      setCities(cities);
      loadCityOptions(cities);
    });
  };

  const loadCityOptions = async (cities: CityDto[]) => {
    const options: { key: any; value: string }[] = [
      { key: "", value: "Select a city" },
      ...cities.map((city) => ({
        key: city.id,
        value: city.name,
      })),
    ];
    setCityOptions(options);
  };

  const loadBlankCityOptions = async () => {
    const options: { key: any; value: string }[] = [
      { key: "", value: "Select a city" },
    ];
    setCityOptions(options);
  };

  const handleChangeSearch = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const value = e.target.value;
    setSearchValue(value);

    if (!value || value === "") {
      setFilteredPeople(people);
      return;
    }

    personService.find(value).then((results) => {
      setFilteredPeople(results);
    });
  };

  const handleCountryChange = (countryId: string) => {
    if (!countryId || countryId === "") {
      setFilteredPeople(people);
      loadCountryOptions(countries);
      setSelectedCountry(undefined);
      loadBlankCityOptions();
      return;
    }

    loadCities(countryId);

    const findPeople = people.filter((x) => x.countryId === countryId);
    setFilteredPeople(findPeople);

    const findCountry = countries.find((x) => x.id === countryId);
    setSelectedCountry(findCountry);
  };

  const handleCityChange = (cityId: string) => {
    if (!cityId || cityId === "") {
      setFilteredPeople(people);
      setSelectedCity(undefined);
      if (selectedCountry) handleCountryChange(selectedCountry.id);
      return;
    }

    const findPeople = people.filter((x) => x.cityId === cityId);
    setFilteredPeople(findPeople);

    const findCity = cities.find((x) => x.id === cityId);
    setSelectedCity(findCity);
  };

  const onView = async (person: PersonDto) => {
    setSelectedPerson(person);
    setDialogOpen(true);
  };

  const handleDialogClose = () => {
    setDialogOpen(false);
  };

  const viewTableButton = (person: PersonDto) => (
    <Button className="table-button view" onClick={() => onView(person)}>
      <FontAwesomeIcon icon={faEye} />
    </Button>
  );

  return (
    <div>
      <h1>People</h1>
      <div className="search-filter-container">
        <Input
          className="search-input-field"
          type="text"
          name="search"
          value={searchValue}
          onChange={handleChangeSearch}
          placeholder="Search anything..."
          required
        />
        <Dropdown
          name="country"
          options={countryOptions}
          selectedValue={selectedCountry?.id}
          onChange={handleCountryChange}
        />
        <Dropdown
          name="city"
          options={cityOptions}
          selectedValue={selectedCity?.id}
          onChange={handleCityChange}
        />
      </div>
      <Table
        data={filteredPeople}
        columns={columns}
        customButtons={[viewTableButton]}
      />
      <Dialog
        title={selectedPerson?.name + " " + selectedPerson?.surname}
        open={isDialogOpen}
        onClose={handleDialogClose}
      >
        <PersonDetails initialValues={selectedPerson} />
      </Dialog>
    </div>
  );
}

export default PeoplePage;
