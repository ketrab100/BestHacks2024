import React, { ChangeEvent } from "react";
import './ProfileCreation.css'
import { Tag } from "../Models/Interfaces";
import TagComponent from "../Utils/TagComponent";

export default function ProfileCreation() {
    const [name, setName] = React.useState<string>();
    const [surname, setSurname] = React.useState<string>();
    const [selectedImage, setSelectedImage] = React.useState<string | null>(null);
    const [description, setDescription] = React.useState<string>();
    const [summary, setSummary] = React.useState<string>();
    const [tags, setTags] = React.useState<Tag[]>([]);
    const allTags: Tag[] = [
        {
            id: "1",
            name: "Java",
            userTags: [],
            jobTags: []
        },
        {
            id: "2",
            name: "JavaScript",
            userTags: [],
            jobTags: []
        },
        {
            id: "3",
            name: "Junior",
            userTags: [],
            jobTags: []
        },
        {
            id: "4",
            name: "Regular",
            userTags: [],
            jobTags: []
        },
        {
            id: "5",
            name: "Senior",
            userTags: [],
            jobTags: []
        },
    ]

    // Obsługa wyboru pliku
    const handleImageChange = (event: ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];  // Sprawdzamy, czy plik został wybrany
        if (file) {
            // Tworzymy tymczasowy URL dla wybranego zdjęcia
            const imageUrl = URL.createObjectURL(file);
            setSelectedImage(imageUrl);
        }
    };

    const updateTags = async (tagName: string) => {
        const tag = allTags.find(tag => tag.name === tagName);
        setTags([...tags, tag!]);
    }

    const onSubmit = () => {
        console.log(
            selectedImage,
            name,
            surname,
            tags,
            description,
            summary
        )
    }


    return (
        <div className="scroll-container">
            <div>
                <h2>Choose profile picture</h2>
                {/* Input do wyboru pliku */}
                <input type="file" accept="image/*" onChange={handleImageChange} />
                {/* Podgląd wybranego zdjęcia */}
                {selectedImage && (
                    <div style={{ marginTop: '20px' }}>
                        <h3>Picture preview:</h3>
                        <img
                            src={selectedImage}
                            alt="Podgląd zdjęcia profilowego"
                            style={{ width: '150px', height: '150px', objectFit: 'cover', borderRadius: '50%' }}
                        />
                    </div>
                )}
            </div>
            <div className="input-field name">
                <span className="title">
                    Name:
                </span>
                <input type="text" className="short-input" value={name} onChange={e => { setName(e.target.value) }} />
            </div>
            <div className="input-field surname">
                <span className="title">
                    Surname:
                </span>
                <input type="text" className="short-input" value={surname} onChange={e => { setSurname(e.target.value) }}></input>
            </div>
            <ul className="tags" aria-label="Select skills that suit your needs the best">
                <select onChange={e => updateTags(e.target.value)}>
                    {allTags.map((tag) => {
                        if (tags?.indexOf(tag) < -1) {
                            console.log(tags, tag)
                            return;
                        }
                        else
                            return (<option value={tag.name}
                                key={tag.id}>{tag.name}
                            </option>)
                    })}
                </select>
                <div className="tagSection">
                    {tags!.map((tag) => {
                        return (
                            <div className="singleTag" key={tag.id}>
                                <TagComponent tag={tag} />
                                <span>|</span>
                            </div>
                        )
                    })}
                </div>
            </ul>
            <div className="input-field description">
                <span className="title">
                    Description:
                </span>
                <textarea className="long-input" value={description} onChange={e => { setDescription(e.target.value) }}></textarea>
            </div>
            <div className="input-field career-summary">
                <span className="title">
                    Career Summary:
                </span>
                <textarea className="long-input" value={summary} onChange={e => { setSummary(e.target.value) }}></textarea>
            </div>
            <button className="submit-button" onClick={onSubmit}>Submit</button>
        </div>
    )
}