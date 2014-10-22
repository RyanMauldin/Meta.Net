/*
 * MySqlModuleTokenizer.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 *
 * Copyright (c) 2009 Developer Performance LLC. All rights reserved.
 */

using System.IO;

using PerCederberg.Grammatica.Runtime;

namespace Meta.Net.MySql.MySqlModuleParser
{
    public class MySqlModuleTokenizer : Tokenizer
    {
         /**
         * <summary>Creates a new tokenizer for the specified input
         * stream.</summary>
         *
         * <param name='input'>the input stream to read</param>
         *
         * <exception cref='ParserCreationException'>if the tokenizer
         * couldn't be initialized correctly</exception>
         */
        public MySqlModuleTokenizer(TextReader input)
            : base(input, false) {

            CreatePatterns();
        }

        /**
         * <summary>Initializes the tokenizer by creating all the token
         * patterns.</summary>
         *
         * <exception cref='ParserCreationException'>if the tokenizer
         * couldn't be initialized correctly</exception>
         */
        private void CreatePatterns() {
            TokenPattern  pattern;

            pattern = new TokenPattern((int) MySqlModuleConstants.MULTI_LINE_COMMENT,
                                       "MULTI_LINE_COMMENT",
                                       TokenPattern.PatternType.REGEXP,
                                       "/\\*([^*]|\\*+[^*/])*\\*+/");
            AddPattern(pattern);

            pattern = new TokenPattern((int) MySqlModuleConstants.SINGLE_LINE_COMMENT,
                                       "SINGLE_LINE_COMMENT",
                                       TokenPattern.PatternType.REGEXP,
                                       "--.*");
            AddPattern(pattern);

            pattern = new TokenPattern((int) MySqlModuleConstants.WHITESPACE,
                                       "WHITESPACE",
                                       TokenPattern.PatternType.REGEXP,
                                       "[ \\t\\n\\r]+");
            AddPattern(pattern);

            pattern = new TokenPattern((int) MySqlModuleConstants.DEFINITION,
                                       "DEFINITION",
                                       TokenPattern.PatternType.REGEXP,
                                       "[Cc][Rr][Ee][Aa][Tt][Ee]([\\r\\n]|.)+");
            AddPattern(pattern);
        }
    }
}
