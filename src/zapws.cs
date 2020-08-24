/*
 * Copyright (c) 2019, 2020 Scott Bennett <scottb@fastmail.com>
 *
 * Permission to use, copy, modify, and distribute this software for any
 * purpose with or without fee is hereby granted, provided that the above
 * copyright notice and this permission notice appear in all copies.
 *
 * THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
 * WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
 * ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
 * WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
 * ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
 * OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libcmdline;

/// <summary>
/// Display or remove trailing whitespace from a file.
/// </summary>
public class Program
{
	private const string program = "zapws.exe";

	/*
	 * TODO:
	 *  - use my command line args library
	 *  - implement mode to delete white space from the file
	 *  - implement option to show full file vs. only the affected lines
	 */
	public static void Main(string[] args)
	{
		if (args.Length < 1) {
			Usage();
		}

		string path = null;

		CommandLineProcessor argsProcessor = new CommandLineProcessor();
		argsProcessor.RegisterOptionMatchHandler("f", requiresArgument: true, (sender, o) => {
			path = o.Argument;
		});
		argsProcessor.RegisterOptionMatchHandler(CommandLineProcessor.InvalidOptionIdentifier, (sender, o) => {
			Usage();
		});
		argsProcessor.ProcessCommandLineArgs(args);

		if (string.IsNullOrEmpty(path) || !File.Exists(path)) {
			Console.WriteLine("invalid path or not a file: {0}", path);
			Quit();
		}

		FileInfo file = new FileInfo(path);
		Print(file);
	}

	public static void Print(FileInfo file)
	{
		using (FileStream stream = file.OpenRead())
		using (StreamReader fileReader = new StreamReader(stream)) {
			int lineno = 0;
			string line;
			while ((line = fileReader.ReadLine()) != null) {
				lineno++;
				char[] array = line.ToCharArray();
				if (array.Length < 1) {
					continue;
				}
				if (char.IsWhiteSpace(array[array.Length - 1])) {
					for (int i = array.Length; i > 0; i--) {
						if (!char.IsWhiteSpace(array[i - 1]))
							break;

						array[i - 1] = '*';
					}

					Console.Write("{0,4}  ", lineno);
					Console.WriteLine(array);
				}
				else {
					//Console.WriteLine(line);
				}
			}
			fileReader.Close();
		}
	}

	/// <summary>
	/// Exit program with exit status 1.
	/// </summary>
	public static void Quit()
	{
		Environment.Exit(1);
	}

	/// <summary>
	/// Print usage information to the console and terminate the app.
	/// </summary>
	public static void Usage()
	{
		Console.WriteLine($"usage: {program} -f filepath");
		Environment.Exit(1);
	}
}
