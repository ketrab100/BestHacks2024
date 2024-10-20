import React, { ChangeEvent, useEffect } from "react";
import './ProfileCreation.css';
import { Employee, Tag } from "../Models/Interfaces";
import TagComponent from "../Utils/TagComponent";
import { getMyData, updateEmployee } from "../Api/Employee";
import { getAllTags } from "../Api/Tag";

export default function ProfileCreation() {
    const [name, setName] = React.useState<string>("");
    const [surname, setSurname] = React.useState<string>("");
    const [id, setId] = React.useState<string>("");
    const [selectedImage, setSelectedImage] = React.useState<string | null>(null);
    const [description, setDescription] = React.useState<string>("");
    const [summary, setSummary] = React.useState<string>("");
    const [allTags, setAllTags] = React.useState<Tag[]>([]); // Stan do przechowywania tagów
    const [tags, setTags] = React.useState<Tag[]>([]); // Wybrane tagi
    const [selectedImageBase64, setSelectedImageBase64] = React.useState<string | null>(null);

    const fetchTags = async () => {
        try {
            const tagsFromApi = await getAllTags(); // Pobierz tagi
            setAllTags(tagsFromApi); // Ustaw tagi w stanie
        } catch (error) {
            console.error("Error fetching tags:", error); // Obsługa błędu
        }
    };

    // Fetch employee data on component mount
    useEffect(() => {
        const fetchEmployeeData = async () => {
            try {
                const employeeData = await getMyData();
                // Ustaw dane pracownika w stanie
                setName(employeeData.firstName);
                setSurname(employeeData.lastName);
                setSelectedImage(employeeData.imageBase64);
                setDescription(employeeData.bio);
                setSummary(employeeData.experience);
                setTags(employeeData.tags);
                setId(employeeData.id);
                setSelectedImage(employeeData.imageBase64);
            } catch (error) {
                console.error("Failed to fetch employee data:", error);
            }
        };

        fetchEmployeeData();
        fetchTags();
    }, []); // Pusta tablica oznacza, że efekt wykona się tylko raz przy montażu

    const handleImageChange = (event: ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (file) {
            const reader = new FileReader();
            reader.readAsDataURL(file); // Odczyt pliku jako base64 string
            reader.onloadend = () => {
                const base64String = reader.result as string;
                setSelectedImageBase64(base64String); // Ustawienie obrazu w formacie base64
            };
        }
    };

    const updateTags = (tagName: string) => {
        const tag = allTags.find(tag => tag.name === tagName);
        if (tag && !tags.includes(tag)) {
            setTags([...tags, tag]);
        }
    };

    const onSubmit = async () => {
        const employeeData: Employee = {
            id: id, // Jeśli id jest unikalne, zaktualizuj je
            firstName: name,
            lastName: surname,
            email: "", // Dodaj odpowiednie pole, jeśli jest wymagane
            bio: description,
            location: "", // Dodaj odpowiednie pole, jeśli jest wymagane
            experience: summary,
            imageBase64: selectedImage || "", // Jeśli obrazek nie jest ustawiony, wyślij pusty string
            tags: tags,
        };

        try {
            const updatedEmployee = await updateEmployee(employeeData);
            console.log("Employee updated successfully:", updatedEmployee);
            // Możesz dodać logikę, np. powiadomienie o pomyślnym zaktualizowaniu profilu
        } catch (error) {
            console.error("Failed to update employee:", error);
            // Możesz dodać logikę do obsługi błędów
        }
    };

    return (
        <div className="scroll-container">
            <div>
                <h2>Choose profile picture</h2>
                <input type="file" accept="image/*" onChange={handleImageChange} />
                {selectedImageBase64 && (
                    <div style={{ marginTop: '20px' }}>
                        <h3>Picture preview:</h3>
                        <img
                            src={selectedImageBase64}
                            alt="Podgląd zdjęcia profilowego"
                            style={{ width: '150px', height: '150px', objectFit: 'cover', borderRadius: '50%' }}
                        />
                    </div>
                )}
            </div>
            <div className="input-field name">
                <span className="title">Name:</span>
                <input type="text" className="short-input" value={name} onChange={e => { setName(e.target.value); }} />
            </div>
            <div className="input-field surname">
                <span className="title">Surname:</span>
                <input type="text" className="short-input" value={surname} onChange={e => { setSurname(e.target.value); }} />
            </div>
            <ul className="tags" aria-label="Select skills that suit your needs the best">
                <select onChange={e => updateTags(e.target.value)}>
                    {allTags.map(tag => (
                        <option value={tag.name} key={tag.id}>{tag.name}</option>
                    ))}
                </select>
                <div className="tagSection">
                    {tags.map(tag => (
                        <div className="singleTag" key={tag.id}>
                            <TagComponent tag={tag} />
                            <span>|</span>
                        </div>
                    ))}
                </div>
            </ul>
            <div className="input-field description">
                <span className="title">Description:</span>
                <textarea className="long-input" value={description} onChange={e => { setDescription(e.target.value); }} />
            </div>
            <div className="input-field career-summary">
                <span className="title">Career Summary:</span>
                <textarea className="long-input" value={summary} onChange={e => { setSummary(e.target.value); }} />
            </div>
            <button className="submit-button" onClick={onSubmit}>Submit</button>
        </div>
    );
}
