/*
 * SqlServerModuleTokenizer.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 *
 * Copyright (c) 2009 Developer Performance LLC. All rights reserved.
 */

using System.IO;

using PerCederberg.Grammatica.Runtime;

namespace Meta.Net.SqlServer.SqlServerModuleParser
{
    /**
     * <remarks>A character stream tokenizer.</remarks>
     */
    public class SqlServerModuleTokenizer : Tokenizer
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
        public SqlServerModuleTokenizer(TextReader input)
            : base(input, false)
        {

            CreatePatterns();
        }

        /**
         * <summary>Initializes the tokenizer by creating all the token
         * patterns.</summary>
         *
         * <exception cref='ParserCreationException'>if the tokenizer
         * couldn't be initialized correctly</exception>
         */
        private void CreatePatterns()
        {
            TokenPattern pattern;

            pattern = new TokenPattern((int)SqlServerModuleConstants.MULTI_LINE_COMMENT,
                                       "MULTI_LINE_COMMENT",
                                       TokenPattern.PatternType.REGEXP,
                                       "/\\*([^*]|\\*+[^*/])*\\*+/");
            AddPattern(pattern);

            pattern = new TokenPattern((int)SqlServerModuleConstants.SINGLE_LINE_COMMENT,
                                       "SINGLE_LINE_COMMENT",
                                       TokenPattern.PatternType.REGEXP,
                                       "--.*");
            AddPattern(pattern);

            pattern = new TokenPattern((int)SqlServerModuleConstants.WHITESPACE,
                                       "WHITESPACE",
                                       TokenPattern.PatternType.REGEXP,
                                       "[ \\t\\n\\r]+");
            AddPattern(pattern);

            pattern = new TokenPattern((int)SqlServerModuleConstants.DEFINITION,
                                       "DEFINITION",
                                       TokenPattern.PatternType.REGEXP,
                                       "[Cc][Rr][Ee][Aa][Tt][Ee]([\\r\\n]|.)+");
            AddPattern(pattern);
        }
    }
}
