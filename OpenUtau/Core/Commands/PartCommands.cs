﻿using OpenUtau.Core.Ustx;

namespace OpenUtau.Core {
    public abstract class PartCommand : UCommand {
        public readonly UProject project;
        public readonly UPart part;
        public PartCommand(UProject project, UPart part) {
            this.project = project;
            this.part = part;
        }
    }

    public class AddPartCommand : PartCommand {
        public AddPartCommand(UProject project, UPart part) : base(project, part) { }
        public override string ToString() => "Add part";
        public override void Execute() => project.parts.Add(part);
        public override void Unexecute() => project.parts.Remove(part);
    }

    public class RemovePartCommand : PartCommand {
        public RemovePartCommand(UProject project, UPart part) : base(project, part) { }
        public override string ToString() => "Remove parts";
        public override void Execute() => project.parts.Remove(part);
        public override void Unexecute() => project.parts.Add(part);
    }

    public class MovePartCommand : PartCommand {
        readonly int newPos, oldPos, newTrackNo, oldTrackNo;
        public MovePartCommand(UProject project, UPart part, int position, int trackNo) : base(project, part) {
            newPos = position;
            newTrackNo = trackNo;
            oldPos = part.position;
            oldTrackNo = part.trackNo;
        }
        public override string ToString() => "Move parts";
        public override void Execute() {
            part.position = newPos;
            part.trackNo = newTrackNo;
        }
        public override void Unexecute() {
            part.position = oldPos;
            part.trackNo = oldTrackNo;
        }
    }

    public class ResizePartCommand : PartCommand {
        readonly int newDur, oldDur;
        public ResizePartCommand(UProject project, UPart part, int duration) : base(project, part) {
            newDur = duration;
            oldDur = part.Duration;
        }
        public override string ToString() => "Change parts duration";
        public override void Execute() => part.Duration = newDur;
        public override void Unexecute() => part.Duration = oldDur;
    }

    public class RenamePartCommand : PartCommand {
        readonly string newName, oldName;
        public RenamePartCommand(UProject project, UPart part, string name) : base(project, part) {
            newName = name;
            oldName = part.name;
        }
        public override string ToString() => "Rename part";
        public override void Execute() => part.name = newName;
        public override void Unexecute() => part.name = oldName;
    }

}
