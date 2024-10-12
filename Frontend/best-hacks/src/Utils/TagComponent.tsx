import React from "react";
import './TagComponent.css';
import { Tag } from "../Models/Interfaces";
import closeIcon from "../images/close_icon.svg"

export default function TagComponent(props: {tag: Tag, onTagClick?: Function}) {
    return (
        <div className="tagContainer"
        onClick={props.onTagClick ? props.onTagClick() : () => {}}>
            {props.tag.name}
        </div>
    )
}