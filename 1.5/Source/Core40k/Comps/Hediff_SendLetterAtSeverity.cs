﻿using Verse;

namespace Core40k
{
    public class Hediff_SendLetterAtSeverity : HediffComp
    {
        protected const int SeverityUpdateInterval = 500;

        private bool hasSentLetter = false;

        private HediffCompProperties_SendLetterAtSeverity Props => (HediffCompProperties_SendLetterAtSeverity)props;


        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);
            if (Pawn.IsHashIntervalTick(SeverityUpdateInterval))
            {
                if (parent.Severity >= Props.severitySendAt)
                {
                    if (Props.onlySendOnce && hasSentLetter)
                    {
                        return;
                    }
                    Letter_JumpTo letter = new Letter_JumpTo
                    {
                        lookTargets = Pawn,
                        def = Props.letterDef,
                        Text = Props.letter,
                        Label = Props.message,
                    };

                    Find.LetterStack.ReceiveLetter(letter);
                }
            }
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look(ref hasSentLetter, "hasSentLetter", false);
        }
    }
}