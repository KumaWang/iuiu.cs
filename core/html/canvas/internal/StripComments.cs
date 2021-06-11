using System;
using System.Text;

namespace WebGL
{
    // ReSharper disable InconsistentNaming

    class StripComments
    {
        private ParseState _parseState;
        private readonly String _sourceString;
        private readonly int _length;
        private int _position;
        private readonly StringBuilder _builder;

        internal StripComments(String str)
        {
            this._parseState = ParseState.BeginningOfLine;
            this._sourceString = str;
            this._length = str.Length;
            this._position = 0;
            this._builder = new StringBuilder();
            this.parse();
        }

        internal String result()
        {
            return this._builder.ToString();
        }

        private bool hasMoreCharacters()
        {
            return this._position < this._length;
        }

        private void parse()
        {
            while (this.hasMoreCharacters())
            {
                this.process(this.current());
                // process() might advance the position.
                if (this.hasMoreCharacters())
                {
                    this.advance();
                }
            }
        }

        private void process(char c)
        {
            if (isNewline(c))
            {
                // No matter what state we are in, pass through newlines
                // so we preserve line numbers.
                this.emit(c);

                if (this._parseState != ParseState.InMultiLineComment)
                {
                    this._parseState = ParseState.BeginningOfLine;
                }

                return;
            }

            var temp = (char)0;
            switch (this._parseState)
            {
                case ParseState.BeginningOfLine:
                    if (isAsciiSpace(c))
                    {
                        this.emit(c);
                        break;
                    }

                    if (c == '#')
                    {
                        this._parseState = ParseState.InPreprocessorDirective;
                        this.emit(c);
                        break;
                    }

                    // Transition to normal state and re-handle character.
                    this._parseState = ParseState.MiddleOfLine;
                    this.process(c);
                    break;

                case ParseState.MiddleOfLine:
                    if (c == '/' && this.peek(ref temp))
                    {
                        if (temp == '/')
                        {
                            this._parseState = ParseState.InSingleLineComment;
                            this.emit(' ');
                            this.advance();
                            break;
                        }

                        if (temp == '*')
                        {
                            this._parseState = ParseState.InMultiLineComment;
                            // Emit the comment start in case the user has
                            // an unclosed comment and we want to later
                            // signal an error.
                            this.emit('/');
                            this.emit('*');
                            this.advance();
                            break;
                        }
                    }

                    this.emit(c);
                    break;

                case ParseState.InPreprocessorDirective:
                    // No matter what the character is, just pass it
                    // through. Do not parse comments in this state. This
                    // might not be the right thing to do long term, but it
                    // should handle the #error preprocessor directive.
                    this.emit(c);
                    break;

                case ParseState.InSingleLineComment:
                    // The newline code at the top of this function takes care
                    // of resetting our state when we get out of the
                    // single-line comment. Swallow all other characters.
                    break;

                case ParseState.InMultiLineComment:
                    if (c == '*' && this.peek(ref temp) && temp == '/')
                    {
                        this.emit('*');
                        this.emit('/');
                        this._parseState = ParseState.MiddleOfLine;
                        this.advance();
                        break;
                    }

                    // Swallow all other characters. Unclear whether we may
                    // want or need to just emit a space per character to try
                    // to preserve column numbers for debugging purposes.
                    break;
            }
        }

        private bool peek(ref char character)
        {
            if (this._position + 1 >= this._length)
            {
                return false;
            }
            character = this._sourceString[this._position + 1];
            return true;
        }

        private char current()
        {
            return this._sourceString[this._position];
        }

        private void advance()
        {
            ++this._position;
        }

        private static bool isNewline(char character)
        {
            // Don't attempt to canonicalize newline related characters.
            return (character == '\n' || character == '\r');
        }

        private void emit(char character)
        {
            this._builder.Append(character);
        }

        private static bool isAsciiSpace(char c)
        {
            return c <= ' ' && (c == ' ' || (c <= 0xD && c >= 0x9));
        }

        private enum ParseState
        {
            BeginningOfLine,
            MiddleOfLine,
            InPreprocessorDirective,
            InSingleLineComment,
            InMultiLineComment
        }
    }

    // ReSharper restore InconsistentNaming
}
